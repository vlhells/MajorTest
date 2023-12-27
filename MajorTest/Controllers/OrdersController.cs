using MajorTest.Dto;
using MajorTest.Models;
using MajorTest.Services.OrderService;
using Microsoft.AspNetCore.Mvc;

namespace MajorTest.Controllers
{
	public class OrdersController : Controller
	{
		private IOrderService _orderService;

		public OrdersController(IOrderService orderService)
		{
			_orderService = orderService;
		}

		[HttpGet]
		public async Task<IActionResult> Index(string searchString)
		{
			return View(await _orderService.IndexAsync(searchString));
		}

		[HttpGet]
		public async Task<IActionResult> SetCourier(int orderId)
		{
			var result = await _orderService.SelectCourier(orderId);
			if (result != null)
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
				var result = await _orderService.SetCourier(orderAndCourier.thisOrderId, 
					orderAndCourier.selectedCourierId);
				if (result)
					return RedirectToAction("Index");
			}

			return BadRequest();
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Order newOrder)
		{
			if (ModelState.IsValid)
			{
				await _orderService.CreateAsync(newOrder);
				return RedirectToAction("Index");
			}

			return BadRequest();
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order != null && order.Status == "Новая")
            {
                return View(order);
            }

            return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Order updatedOrder)
		{
			if (ModelState.IsValid)
			{
				await _orderService.EditAsync(updatedOrder);
				return RedirectToAction("Index");
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
		public async Task<IActionResult> CancelOrder(int id)
		{
			var order = await _orderService.GetOrderByIdAsync(id);
			if (order != null)
			{
				return View(order);
			}

			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> CancelOrder(Order orderToCancel)
		{
			if (!String.IsNullOrEmpty(orderToCancel.CancellationComment))
			{
				var result = await _orderService.CancelOrder(orderToCancel.Id, 
											orderToCancel.CancellationComment);
				if (result)
				{
					return RedirectToAction("Index");
				}
			}

			return BadRequest();
		}

        [HttpGet]
        public async Task<IActionResult> CancellationReason(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order != null)
            {
                return View(order);
            }

            return NotFound();
        }
    }
}
