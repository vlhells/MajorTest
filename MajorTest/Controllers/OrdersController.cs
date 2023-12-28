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
            if (order != null && order.State == Order.OrderStates["new"])
            {
                return View(order);
            }

            return NotFound();
		}

        [ValidateAntiForgeryToken]
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
		public async Task<IActionResult> ChangeState(int id)
		{
			var order = await _orderService.GetOrderState(id);
			if (order != null)
			{
				return View(order);
			}

			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> ChangeState(ChangeOrderStateDto updatedOrder)
		{
			if (ModelState.IsValid)
			{
				var result = await _orderService.ChangeState(updatedOrder.thisOrderId, 
					updatedOrder.selectedState, updatedOrder.cancellationComment);
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
