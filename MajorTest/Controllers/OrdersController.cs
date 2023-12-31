using MajorTest.Dto;
using MajorTest.Models;
using MajorTest.Services.OrdersService;
using Microsoft.AspNetCore.Mvc;

namespace MajorTest.Controllers
{
	public class OrdersController : Controller
	{
		private IOrdersService _orderService;

		public OrdersController(IOrdersService orderService)
		{
			_orderService = orderService;
		}

		[HttpGet]
		public async Task<IActionResult> Index(string? searchString, int page = 1)
		{
			return View(await _orderService.IndexAsync(searchString, page));
		}

		[HttpGet]
		public async Task<IActionResult> SetCourier(int orderId)
		{
			var order = await _orderService.GetOrderByIdAsync(orderId);
			var result = await _orderService.SelectCourier(orderId);
			if (result != null && order != null && order.State == Order.OrderStates["new"])
			{
				return View(result);
			}

			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> SetCourier(SelectCourierDto orderAndCourier)
		{
			if (ModelState.IsValid)
			{
				Order order = await _orderService.GetOrderByIdAsync(orderAndCourier.ThisOrderId);
                if (order != null && order.State == Order.OrderStates["new"])
                {
                    var result = await _orderService.SetCourier(orderAndCourier.ThisOrderId,
                        orderAndCourier.SelectedCourierId);
                    if (result)
                        return RedirectToAction("Index");
                }
            }

			return BadRequest();
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(OrderDto newOrderData)
		{
			if (ModelState.IsValid)
			{
				await _orderService.CreateAsync(newOrderData);
				return RedirectToAction("Index");
			}

			return BadRequest();
		}

        [HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
            var order = await _orderService.GetOrderByIdAsync(id);
			var orderDto = new OrderDto(order.Item, order.ItemSender, order.ItemReceiver,
				order.MeetingTime, order.MeetingPlace, order.TargetAddress);

            if (order != null && order.State == Order.OrderStates["new"])
            {
                return View(orderDto);
            }

            return NotFound();
		}

        [ValidateAntiForgeryToken]
        [HttpPost]
		public async Task<IActionResult> Edit(OrderDto updatedOrderData)
		{
			if (ModelState.IsValid)
			{
				Order order = await _orderService.GetOrderByIdAsync(updatedOrderData.Id);
                if (order != null && order.State == Order.OrderStates["new"])
                {
                    await _orderService.EditAsync(updatedOrderData);
                    return RedirectToAction("Index");
                }
            }

			return BadRequest();
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id)
        {
			var result = await _orderService.DeleteAsync(id);
			if (result)
				return RedirectToAction("Index");
			else
				return NotFound();
        }

		[HttpGet]
		public async Task<IActionResult> ChangeState(int id)
		{
			var orderState = await _orderService.GetOrderState(id);
            if (orderState != null)
			{
				Order order = await _orderService.GetOrderByIdAsync(orderState.ThisOrderId);
                if (order != null && order.State != Order.OrderStates["cancelled"] &&
                order.State != Order.OrderStates["done"])
                {
                    return View(orderState);
                }
            }

			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> ChangeState(ChangeOrderStateDto updatedOrder)
		{
			if (ModelState.IsValid)
			{
				Order order = await _orderService.GetOrderByIdAsync(updatedOrder.ThisOrderId);
                if (order != null && order.State != Order.OrderStates["cancelled"] &&
                order.State != Order.OrderStates["done"])
                {
                    var result = await _orderService.ChangeState(updatedOrder.ThisOrderId,
                        updatedOrder.SelectedState, updatedOrder.CancellationComment);
                    if (result)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            return BadRequest();
		}

        [HttpGet]
        public async Task<IActionResult> CancellationReason(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order != null && order.State == Order.OrderStates["cancelled"])
            {
                return View(order);
            }

            return NotFound();
        }
    }
}
