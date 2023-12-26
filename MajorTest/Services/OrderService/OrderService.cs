using MajorTest.Models;
using Microsoft.EntityFrameworkCore;

namespace MajorTest.Services.OrderService
{
    public class OrderService: IOrderService
    {
        private MajorContext _db;

        public OrderService(MajorContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Order>> IndexAsync(string searchString)
        {
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
                                      o.ItemReceiver.LastName.Contains(searchString));
            }

            return await orders.ToListAsync();
        }

        public async Task CreateAsync(Order newOrder)
        {
            await _db.Orders.AddAsync(newOrder);
            await _db.SaveChangesAsync();
        }

        public async Task EditAsync(Order updatedOrder)
        {
            _db.Orders.Update(updatedOrder);
            await _db.SaveChangesAsync();
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
    }
}
