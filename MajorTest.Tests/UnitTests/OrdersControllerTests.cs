using MajorTest.Controllers;
using MajorTest.Dto;
using MajorTest.Models;
using MajorTest.Services.OrdersService;
using MajorTest.ViewModels;
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
			ordersService.IndexAsync(String.Empty, 1).Returns(ImitateOrdersDataset());
			var controller = new OrdersController(ordersService);

			// Act:
			var result = await controller.Index(String.Empty);

			// Assert:
			var isViewResult = Assert.IsType<ViewResult>(result);
			var model = Assert.IsAssignableFrom<IndexOrderViewModel>(isViewResult.Model);
			Assert.Equal(ImitateOrdersDataset().Orders.Count(), model.Orders.Count());
		}
		private IndexOrderViewModel ImitateOrdersDataset()
		{
			var orders = new List<Order>
			{
				new Order{ItemSender = new ItemSender{ FirstName = "Alexander",
													   SecondName = "D",
													   LastName = "Brown",
													   PhoneNumber = "+79991231519"},
						  Courier = new Courier{ FirstName = "�",
												 SecondName = "�",
												 LastName = "�",
											     PhoneNumber = "+79991231519"},
						  ItemReceiver = new ItemReceiver{ FirstName = "�",
												 SecondName = "�",
												 LastName = "�",
												 PhoneNumber = "+79991231519"}},

				new Order{Courier = new Courier{ FirstName = "Dmitry",
											     SecondName = "M",
												 LastName = "Smith",
										         PhoneNumber = "+79991231519"},
						  ItemSender = new ItemSender{ FirstName = "�",
												 SecondName = "�",
												 LastName = "�", 
												 PhoneNumber = "+79991231519"},
						  ItemReceiver = new ItemReceiver{ FirstName = "�",
												 SecondName = "�",
												 LastName = "�",
												 PhoneNumber = "+79991231519"}
				},

				new Order{ItemReceiver = new ItemReceiver{
													   FirstName = "Miroslava",
													   SecondName = "John",
													   LastName = "White",
													   PhoneNumber = "+79991231519"},
						 ItemSender = new ItemSender{ FirstName = "�",
												 SecondName = "�",
												 LastName = "�",
												 PhoneNumber = "+79991231519"},
						 Courier = new Courier{ FirstName = "�",
												 SecondName = "�",
												 LastName = "�",
												 PhoneNumber = "+79991231519"}}
			};

			return new IndexOrderViewModel { Orders = orders, PageViewModel = new PageViewModel(1, 2, 3)};
		}

		[Fact]
		public async Task IndexReturnsFilteredData()
		{
			// Arrange:
			string searchString = "Dmitry";
			var ordersService = Substitute.For<IOrdersService>();
			ordersService.IndexAsync(searchString, 1).Returns
				(ImitateFiltredDataset(searchString));
			var controller = new OrdersController(ordersService);

			// Act:
			var result = await controller.Index(searchString);

			// Assert:
			var isViewResult = Assert.IsType<ViewResult>(result);
			var model = Assert.IsAssignableFrom<IndexOrderViewModel>(isViewResult.Model);
			Assert.True(ImitateOrdersDataset().Orders.Count() > model.Orders.Count());
		}
		private IndexOrderViewModel ImitateFiltredDataset(string searchString)
		{
            var orders = ImitateOrdersDataset().Orders.Where(o => o.ItemSender.FirstName.Contains(searchString) ||
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
                .ToList();

            return new IndexOrderViewModel { Orders = orders, PageViewModel = new PageViewModel(1, 2, 3) };
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
				new Courier{ FirstName = "������", SecondName = "��������",
							 LastName = "���������", PhoneNumber = "+79991231515"},
				new Courier{ FirstName = "�����", SecondName = "������",
							 LastName = "�������", PhoneNumber = "+79991231516"},
				new Courier{ FirstName = "�", SecondName = "�",
							 LastName = "�", PhoneNumber = "+79991231519"},
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