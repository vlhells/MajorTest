using MajorTest.Controllers;
using MajorTest.Models;
using MajorTest.Services.CouriersService;
using MajorTest.ViewModels;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace UnitTests
{
	public class CouriersControllerTests
	{
		[Fact]
		public async Task IndexReturnsAllCouriersAndCorrectTypes()
		{
			// Arrange:
			var couriersService = Substitute.For<ICouriersService>();
			couriersService.IndexAsync(String.Empty, 1).Returns(ImitateCouriersDataset());
			var controller = new CouriersController(couriersService);

			// Act:
			var result = await controller.Index(String.Empty);

			// Assert:
			var isViewResult = Assert.IsType<ViewResult>(result);
			var model = Assert.IsAssignableFrom<IndexCourierViewModel>(isViewResult.Model);
			Assert.Equal(ImitateCouriersDataset().Couriers.Count(), model.Couriers.Count());
		}
		private IndexCourierViewModel ImitateCouriersDataset()
		{
			var couriers = new List<Courier>
			{
				new Courier{ FirstName = "Андрей", SecondName = "Игоревич",
							 LastName = "Федорович", PhoneNumber = "+79991231515"},
				new Courier{ FirstName = "Мирон", SecondName = "Янович",
							 LastName = "Федоров", PhoneNumber = "+79991231516"},
				new Courier{ FirstName = "а", SecondName = "б",
							 LastName = "в", PhoneNumber = "+79991231519"},
			};

			return new IndexCourierViewModel { Couriers = couriers, PageViewModel = new PageViewModel(1, 2, 3)};
		}
        [Fact]
        public async Task IndexReturnsFilteredData()
        {
            // Arrange:
            string searchString = "Dmitry";
            var courierService = Substitute.For<ICouriersService>();
            courierService.IndexAsync(searchString, 1).Returns
                (ImitateFiltredDataset(searchString));
            var controller = new CouriersController(courierService);

            // Act:
            var result = await controller.Index(searchString);

            // Assert:
            var isViewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IndexCourierViewModel>(isViewResult.Model);
            Assert.True(ImitateCouriersDataset().Couriers.Count() > model.Couriers.Count());
        }
        private IndexCourierViewModel ImitateFiltredDataset(string searchString)
        {
            var couriers = ImitateCouriersDataset().Couriers.Where(o => o.FirstName.Contains(searchString) ||
                                      o.SecondName.Contains(searchString) ||
                                      o.LastName.Contains(searchString) ||
                                      o.FirstName.Contains(searchString) ||
                                      o.SecondName.Contains(searchString) ||
                                      o.LastName.Contains(searchString) ||
                                      o.FirstName.Contains(searchString) ||
                                      o.SecondName.Contains(searchString) ||
                                      o.LastName.Contains(searchString) ||
                                      o.PhoneNumber.Contains(searchString) ||
                                      o.PhoneNumber.Contains(searchString) ||
                                      o.PhoneNumber.Contains(searchString))
                .ToList();

            return new IndexCourierViewModel { Couriers = couriers, PageViewModel = new PageViewModel(1, 2, 3) };
        }

        [Fact]
		public async Task POSTCreateReturnsRedirToAct()
		{
			// Arrange:
			var couriersService = Substitute.For<ICouriersService>();
			var courier = new Courier();
			var controller = new CouriersController(couriersService);

			// Act:
			var result = await controller.Create(courier);

			// Assert:
			Assert.IsType<RedirectToActionResult>(result);
		}

		[Fact]
		public async Task GETEditReturnsCourier()
		{
			// Arrange:
			var couriersService = Substitute.For<ICouriersService>();
			var courier = new Courier();
			couriersService.GetCourierByIdAsync(1).Returns(courier);
			var controller = new CouriersController(couriersService);

			// Act:
			var result = await controller.Edit(1);

			// Assert:
			var isViewResult = Assert.IsType<ViewResult>(result);
			Assert.IsAssignableFrom<Courier>(isViewResult.Model);
		}

		[Fact]
		public async Task POSTEditReturnsRedirToAct()
		{
			// Arrange:
			var couriersService = Substitute.For<ICouriersService>();
			var courier = new Courier();
			var controller = new CouriersController(couriersService);

			// Act:
			var result = await controller.Edit(courier);

			// Assert:
			Assert.IsType<RedirectToActionResult>(result);
		}

		[Fact]
		public async Task DeleteReturnsTrueAndRedirToAct()
		{
			// Arrange:
			var couriersService = Substitute.For<ICouriersService>();
			couriersService.DeleteAsync(1).Returns(true);
			var controller = new CouriersController(couriersService);

			// Act:
			var result = await controller.Delete(1);

			// Assert:
			Assert.IsType<RedirectToActionResult>(result);
		}
	}
}
