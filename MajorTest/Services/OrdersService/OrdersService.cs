using MajorTest.Dto;
using MajorTest.Models;
using MajorTest.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MajorTest.Services.OrdersService
{
    public class OrdersService: IOrdersService
    {
        private MajorContext _db;

        public OrdersService(MajorContext db)
        {
            _db = db;
        }

        public async Task<IndexOrderViewModel> IndexAsync(string? searchString, int page)
        {
            int pageSize = 20;

            IQueryable<Order> orders = _db.Orders.AsNoTracking()
                         .Include(o => o.Item)
                         .Include(o => o.ItemSender)
                         .Include(o => o.Courier)
                         .Include(o => o.ItemReceiver);

			if (!String.IsNullOrEmpty(searchString))
			{
				orders = orders.Where(o => o.ItemSender.FirstName.Contains(searchString) ||
									  o.ItemSender.SecondName.Contains(searchString) ||
									  o.ItemSender.LastName.Contains(searchString) ||
									  o.Courier.FirstName.Contains(searchString) ||
									  o.Courier.SecondName.Contains(searchString) ||
									  o.Courier.LastName.Contains(searchString) ||
									  o.ItemReceiver.FirstName.Contains(searchString) ||
									  o.ItemReceiver.SecondName.Contains(searchString) ||
									  o.ItemReceiver.LastName.Contains(searchString) ||
									  o.ItemSender.PhoneNumber.Contains(searchString) ||
									  o.Courier.PhoneNumber.Contains(searchString) ||
									  o.ItemReceiver.PhoneNumber.Contains(searchString));
			}

			var count = await orders.CountAsync();
			var items = await orders.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

			PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexOrderViewModel indexOrderViewModel = new IndexOrderViewModel
            {
                PageViewModel = pageViewModel,
                Orders = items
            };

            return indexOrderViewModel;
		}

        public async Task CreateAsync(Order newOrder)
        {
            await _db.Orders.AddAsync(newOrder);
            await _db.SaveChangesAsync();
        }

        public async Task EditAsync(Order updatedOrder)
        {
            if (updatedOrder != null)
            { 
                var thisOrder = await _db.Orders.FindAsync(updatedOrder.Id);
                if (thisOrder.State == Order.OrderStates["new"])
                {
                    using (var transaction = _db.Database.BeginTransaction())
                    {
                        try
                        {
                            var originalOrderFromDb = await _db.Orders.Include(o => o.ItemSender)
                                                                .Include(o => o.Item)
                                                                .Include(o => o.Courier)
                                                                .Include(o => o.ItemReceiver)
                                                                .FirstOrDefaultAsync(o => o.Id == updatedOrder.Id);
                            if (originalOrderFromDb != null)
                            {
                                originalOrderFromDb.TargetAddress = updatedOrder.TargetAddress;
                                originalOrderFromDb.MeetingPlace = updatedOrder.MeetingPlace;
                                originalOrderFromDb.MeetingTime = updatedOrder.MeetingTime;
                                originalOrderFromDb.ItemSender = updatedOrder.ItemSender;
                                originalOrderFromDb.Item = updatedOrder.Item;
                                originalOrderFromDb.Courier = updatedOrder.Courier;
                                originalOrderFromDb.ItemReceiver = updatedOrder.ItemReceiver;
                                await _db.SaveChangesAsync();
                                await transaction.CommitAsync();
                            }
                        }
                        catch (Exception ex)
                        {
                            await transaction.RollbackAsync();
                        }
                    }
                }
            }
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _db.Orders.Include(o => o.Item)
                                   .Include(o => o.ItemSender)
                                   .Include(o => o.Courier)
                                   .Include(o => o.ItemReceiver)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var targetOrder = await _db.Orders.FindAsync(id);
            if (targetOrder != null)
            {
                _db.Orders.Remove(targetOrder);
                await _db.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<SelectCourierDto> SelectCourier(int orderId)
        {
            var allCouriers = _db.Couriers;
            var thisOrder = await _db.Orders.Include(o => o.Courier).FirstOrDefaultAsync(o => o.Id == orderId);

            if (thisOrder != null && thisOrder.Courier == null && allCouriers != null)
            {
                var couriersForSelectList = await allCouriers.Select(c => new
                {
                    c.Id,
                    DisplayName = $"{c.FirstName} {c.SecondName} {c.LastName}"
                }).ToListAsync();

                return new SelectCourierDto { AllCouriers = new SelectList(couriersForSelectList, 
                    "Id", "DisplayName"), ThisOrderId = thisOrder.Id };
            }

            return null;
        }

        public async Task<bool> SetCourier(int orderId, int courierId)
        {
			var targetOrder = await _db.Orders.Include(o => o.Courier).
                                    FirstOrDefaultAsync(o => o.Id == orderId);

            var targetCourier = await _db.Couriers.FindAsync(courierId);

            if (targetOrder != null && targetCourier != null)
            {
                targetOrder.Courier = targetCourier;
                targetOrder.State = Order.OrderStates["inProcess"];
                await _db.SaveChangesAsync();
                return true;
            }
            else
                return false;
		}

        public async Task<ChangeOrderStateDto> GetOrderState(int orderId)
        {
            var thisOrder = await _db.Orders.Include(o => o.Courier).FirstOrDefaultAsync(o => o.Id == orderId);

            if (thisOrder != null)
            {
                ChangeOrderStateDto dto = new ChangeOrderStateDto();

                if (thisOrder.State == Order.OrderStates["new"])
                {
                    var statesForSelectList = Order.OrderStates.
                        Where(o => o.Key != "inProcess" && o.Key != "done")
                        // Чтобы нельзя было у нового поставить просто так "Выполнено" или "Передан" без курьера.
                        .Select(o => new
                        {
                            StateKey = o.Key,
                            StateValue = o.Value
                        }).ToList();

                    dto.OrderStates = new SelectList(statesForSelectList, "StateKey", "StateValue");
                    dto.ThisOrderId = thisOrder.Id;
                }
                else
                {
                    var statesForSelectList = Order.OrderStates.
                        Where(o => o.Key != "inProcess" && o.Key != "new")
                        .Select(o => new
                        {
                            StateKey = o.Key,
                            StateValue = o.Value
                        }).ToList();

                    dto.OrderStates = new SelectList(statesForSelectList, "StateKey", "StateValue");
                    dto.ThisOrderId = thisOrder.Id;
                }

                return dto;
            }

            return null;
        }

        public async Task<bool> ChangeState(int orderId, string newState, string? cancellationComment)
        {
			var order = await _db.Orders.FindAsync(orderId);
			if (order != null)
			{
				order.State = Order.OrderStates[newState];
                if (order.State == Order.OrderStates["cancelled"])
				    order.CancellationComment = cancellationComment;
				await _db.SaveChangesAsync();
				return true;
			}

			return false;
        }
    }
}
