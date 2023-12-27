using MajorTest.Controllers;
using MajorTest.Dto;
using MajorTest.Models;
using MajorTest.Services.OrdersService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NSubstitute;

namespace UnitTests
{
	public class OrdersControllerTests
	{
		[Fact]
		public async Task IndexReturnsAllOrdersAndCorrectTypes()
		{
			// Arrange:
			var ordersService = Substitute.For<IOrdersService>();
			ordersService.IndexAsync(String.Empty).Returns(ImitateOrdersDataset());
			var controller = new OrdersController(ordersService);

			// Act:
			var result = await controller.Index(String.Empty);

			// Assert:
			var isViewResult = Assert.IsType<ViewResult>(result);
			var model = Assert.IsAssignableFrom<IEnumerable<Order>>(isViewResult.Model);
			Assert.Equal(ImitateOrdersDataset().Count(), model.Count());
		}
		private List<Order> ImitateOrdersDataset()
		{
			var orders = new List<Order>
			{
				new Order{ItemSender = new ItemSender{ FirstName = "Александр",
													   SecondName = "Борисович",
													   LastName = "Зубенко",
													   PhoneNumber = "+79991231519"},
						  Courier = new Courier{ FirstName = "а",
												 SecondName = "б",
												 LastName = "в",
											     PhoneNumber = "+79991231519"},
						  ItemReceiver = new ItemReceiver{ FirstName = "а",
												 SecondName = "б",
												 LastName = "в",
												 PhoneNumber = "+79991231519"}},

				new Order{Courier = new Courier{ FirstName = "Борис",
											     SecondName = "Миронович",
												 LastName = "Станиславов",
										         PhoneNumber = "+79991231519"},
						  ItemSender = new ItemSender{ FirstName = "а",
												 SecondName = "б",
												 LastName = "в", 
												 PhoneNumber = "+79991231519"},
						  ItemReceiver = new ItemReceiver{ FirstName = "а",
												 SecondName = "б",
												 LastName = "в",
												 PhoneNumber = "+79991231519"}
				},

				new Order{ItemReceiver = new ItemReceiver{
													   FirstName = "Дмитрий",
													   SecondName = "Дамирович",
													   LastName = "КПСС",
													   PhoneNumber = "+79991231519"},
						 ItemSender = new ItemSender{ FirstName = "а",
												 SecondName = "б",
												 LastName = "в",
												 PhoneNumber = "+79991231519"},
						 Courier = new Courier{ FirstName = "а",
												 SecondName = "б",
												 LastName = "в",
												 PhoneNumber = "+79991231519"}}
			};

			return orders;
		}

		[Fact]
		public async Task IndexReturnsFilteredData()
		{
			// Arrange:
			string searchString = "Александр";
			var ordersService = Substitute.For<IOrdersService>();
			ordersService.IndexAsync(searchString).Returns
				(ImitateOrdersDataset().Where(o => o.ItemSender.FirstName.Contains(searchString) ||
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
									  o.ItemReceiver.PhoneNumber.Contains(searchString))
				.ToList());
			var controller = new OrdersController(ordersService);

			// Act:
			var result = await controller.Index(searchString);

			// Assert:
			var isViewResult = Assert.IsType<ViewResult>(result);
			var model = Assert.IsAssignableFrom<IEnumerable<Order>>(isViewResult.Model);
			Assert.True(ImitateOrdersDataset().Count() > model.Count());
		}

		[Fact]
		public async Task GETSetCourierReturnsSelectCourierDto()
		{
			// Arrange:
			var ordersService = Substitute.For<IOrdersService>();
			var dto = new SelectCourierDto
			{
				allCouriers = ImitateCouriersDataset(),
				selectedCourierId = 1,
				thisOrderId = 1
			};
			ordersService.SelectCourier(dto.thisOrderId).Returns(dto);
			var controller = new OrdersController(ordersService);

			// Act:
			var result = await controller.SetCourier(dto.thisOrderId);

			// Assert:
			var isViewResult = Assert.IsType<ViewResult>(result);
			Assert.IsAssignableFrom<SelectCourierDto>(isViewResult.Model);
		}
		private SelectList ImitateCouriersDataset()
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

			return new SelectList(couriers);
		}

		[Fact]
		public async Task POSTCreateReturnsRedirToAct()
		{
			// Arrange:
			var ordersService = Substitute.For<IOrdersService>();
			Order order = new Order();
			var controller = new OrdersController(ordersService);

			// Act:
			var result = await controller.Create(order);

			// Assert:
			Assert.IsType<RedirectToActionResult>(result);
		}

		[Fact]
		public async Task GETEditReturnsOrder()
		{
			// Arrange:
			var ordersService = Substitute.For<IOrdersService>();
			var order = new Order();
			ordersService.GetOrderByIdAsync(1).Returns(order);
			var controller = new OrdersController(ordersService);

			// Act:
			var result = await controller.Edit(1);

			// Assert:
			var isViewResult = Assert.IsType<ViewResult>(result);
			Assert.IsAssignableFrom<Order>(isViewResult.Model);
		}

		[Fact]
		public async Task POSTEditReturnsRedirToAct()
		{
			// Arrange:
			var ordersService = Substitute.For<IOrdersService>();
			var order = new Order();
			var controller = new OrdersController(ordersService);

			// Act:
			var result = await controller.Edit(order);

			// Assert:
			Assert.IsType<RedirectToActionResult>(result);
		}

		[Fact]
		public async Task DeleteReturnsTrueAndRedirToAct()
		{
			// Arrange:
			var ordersService = Substitute.For<IOrdersService>();
			ordersService.DeleteAsync(1).Returns(true);
			var controller = new OrdersController(ordersService);

			// Act:
			var result = await controller.Delete(1);

			// Assert:
			Assert.IsType<RedirectToActionResult>(result);
		}

		[Fact]
		public async Task GETChangeStateReturnsOrderAndViewResult()
		{
			// Arrange:
			var ordersService = Substitute.For<IOrdersService>();
			var dto = new ChangeOrderStateDto();
			ordersService.GetOrderState(1).Returns(dto);
			var controller = new OrdersController(ordersService);

			// Act:
			var result = await controller.ChangeState(1);

			// Assert:
			var isViewResult = Assert.IsType<ViewResult>(result);
			Assert.IsAssignableFrom<ChangeOrderStateDto>(isViewResult.Model);
		}

		[Fact]
		public async Task POSTChangeStateReturnsRedirToAct()
		{
			// Arrange:
			var ordersService = Substitute.For<IOrdersService>();
			var dto = new ChangeOrderStateDto()
			{
				selectedState = "state",
				cancellationComment = "comment",
				thisOrderId = 1,
			};
			ordersService.ChangeState(dto.thisOrderId, dto.selectedState, dto.cancellationComment)
				.Returns(true);
			var controller = new OrdersController(ordersService);

			// Act:
			var result = await controller.ChangeState(dto);

			// Assert:
			Assert.IsType<RedirectToActionResult>(result);
		}

		[Fact]
		public async Task CancellationReasonReturnsViewResult()
		{
			// Arrange:
			var ordersService = Substitute.For<IOrdersService>();
			var order = new Order();
			ordersService.GetOrderByIdAsync(1).Returns(order);
			var controller = new OrdersController(ordersService);

			// Act:
			var result = await controller.CancellationReason(1);

			// Assert:
			var isViewResult = Assert.IsType<ViewResult>(result);
			Assert.IsAssignableFrom<Order>(isViewResult.Model);
		}
	}
}