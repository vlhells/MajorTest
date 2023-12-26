using MajorTest.Models;
using MajorTest.Services.OrderService;
using Microsoft.AspNetCore.Mvc;

namespace MajorTest.Controllers
{
	public class OrderController : Controller
	{
		private IOrderService _orderService;

		public OrderController(IOrderService orderService)
		{
			_orderService = orderService;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			return View(await _orderService.IndexAsync());
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
            if (order != null)
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
    }
}
