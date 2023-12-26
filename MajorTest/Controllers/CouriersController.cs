using MajorTest.Models;
using MajorTest.Services.CourierService;
using Microsoft.AspNetCore.Mvc;

namespace MajorTest.Controllers
{
    public class CouriersController : Controller
    {
        private ICourierService _courierService;

        public CouriersController(ICourierService courierService)
        {
            _courierService = courierService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            return View(await _courierService.IndexAsync(searchString));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Courier newCourier)
        {
            if (ModelState.IsValid)
            {
                await _courierService.CreateAsync(newCourier);
                return RedirectToAction("Index");
            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var courier = await _courierService.GetCourierByIdAsync(id);
            if (courier != null)
            {
                return View(courier);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Courier updatedCourier)
        {
            if (ModelState.IsValid)
            {
                await _courierService.EditAsync(updatedCourier);
                return RedirectToAction("Index");
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _courierService.DeleteAsync(id);
            if (result)
                return RedirectToAction("Index");
            else
                return NotFound();
        }
    }
}
