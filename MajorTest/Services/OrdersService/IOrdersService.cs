using MajorTest.Dto;
using MajorTest.Models;
using MajorTest.ViewModels;

namespace MajorTest.Services.OrdersService
{
    public interface IOrdersService
    {
		public Task<IndexOrderViewModel> IndexAsync(string? searchString, int page);
		public Task CreateAsync(Order newOrder);
        public Task EditAsync(Order updatedOrder);
        public Task<Order> GetOrderByIdAsync(int id);
        public Task<bool> DeleteAsync(int id);
        /// <summary>
        /// Uses for POST-method to set courier for order.
        /// </summary>
        public Task<bool> SetCourier(int orderId, int courierId);

        /// <summary>
        /// Uses for GET-method to pass dto with couriers into View.
        /// </summary>
        public Task<SelectCourierDto> SelectCourier(int orderId);

        public Task<bool> ChangeState(int orderId, string newState, string? cancellationComment);

        /// <summary>
        /// Uses for GET-method to pass dto for select orderState into View.
        /// </summary>
        public Task<ChangeOrderStateDto> GetOrderState(int orderId);

    }
}
