using MajorTest.Dto;
using MajorTest.Models;
using MajorTest.ViewModels;

namespace MajorTest.Services.OrdersService
{
    /// <summary>
    /// The interface determines service for work with an <see cref="Order"/> objects.
    /// </summary>
    public interface IOrdersService
    {
        /// <summary>
        /// Returns a <see cref="IndexOrderViewModel"/> object with the content (info about orders)
        /// on target page (pagination)
        /// by specifying a <paramref name="searchString"/> and <paramref name="page"/>.
        /// </summary>
        /// <param name="searchString">String (key-word) for search content (checks Full-name columns
        /// and phone numbers).</param>
        /// <param name="page">Num of target page in pagination.</param>
        /// <returns>An <see cref="IndexOrderViewModel"/> object.</returns>
        public Task<IndexOrderViewModel> IndexAsync(string? searchString, int page);

        /// <summary>
        /// Creates an <see cref="Order"/> object in <see cref="MajorContext.Orders"/> 
        /// via Entity Framework Core abilities with info
        /// from frontend (GET-method Create): <paramref name="newOrderData"/>.
        /// </summary>
        /// <param name="newOrderData">OrderDto got from GET-method Create (frontend).</param>
		public Task CreateAsync(OrderDto newOrderData);

        /// <summary>
        /// Updates existing <see cref="Order"/> object in <see cref="MajorContext.Orders"/> 
        /// via Entity Framework Core abilities with new data
        /// from frontend (GET-method Create): <paramref name="updatedOrderData"/>.
        /// </summary>
        /// <param name="updatedOrderData">OrderDto with updated data got from GET-method Edit (frontend).</param>
        public Task EditAsync(OrderDto updatedOrderData);

        /// <summary>
        /// Returns an <see cref="Order"/> object with its' internal content 
        /// by specifying a <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The integer order identifier.</param>
        /// <returns>An <see cref="Order"/> object.</returns>
        public Task<Order> GetOrderByIdAsync(int? id);

        /// <summary>
        /// Returns a <see cref="bool"/> value by specifying an <paramref name="id"/>.
        /// <see cref="true"/> if <see cref="Order"/> object with this <paramref name="id"/>
        /// has been sucessfully deleted from <see cref="MajorContext.Orders"/>, otherwise, <see cref="false"/>.
        /// </summary>
        /// <param name="id">The integer order identifier.</param>
        /// <returns>The <see cref="bool"/> value.</returns>
        public Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Returns a <see cref="bool"/> value by specifying an <paramref name="orderId"/> and 
        /// <paramref name="courierId"/>.
        /// <see cref="true"/> if <see cref="Courier"/> object with <paramref name="courierId"/>
        /// has been sucessfully set for <see cref="Order"/> with <paramref name="orderId"/>, otherwise, 
        /// <see cref="false"/>.
        /// </summary>
        /// <param name="orderId">The integer order identifier.</param>
        /// <param name="courierId">The integer courier identifier.</param>
        /// <returns>The <see cref="bool"/> value.</returns>
        public Task<bool> SetCourier(int orderId, int courierId);

        /// <summary>
        /// Returns a <see cref="SelectCourierDto"/> model with the some data for select courier in frontend
        /// by specifying an <paramref name="orderId"/>. Uses by GET-method.
        /// </summary>
        /// <param name="orderId">The integer order identifier.</param>
        /// <returns>The <see cref="SelectCourierDto"/> object.</returns>
        public Task<SelectCourierDto> SelectCourier(int orderId);

        /// <summary>
        /// Returns a <see cref="bool"/> value by specifying an <paramref name="orderId"/>,  
        /// <paramref name="newState"/>, <paramref name="cancellationComment"/>.
        /// <see cref="true"/> if <see cref="Order.State"/> successfully changed, otherwise, 
        /// <see cref="false"/>.
        /// </summary>
        /// <param name="orderId">The integer order identifier.</param>
        /// <param name="newState">String with key of new <see cref="Order.State"/> from 
        /// <see cref="Order.OrderStates"/>.</param>
        /// <param name="cancellationComment">Comment (if new <see cref="Order.State"/> == "cancelled").</param>
        /// <returns>The <see cref="bool"/> value.</returns>
        public Task<bool> ChangeState(int orderId, string newState, string? cancellationComment);

        /// <summary>
        /// Returns a <see cref="ChangeOrderStateDto"/> model with the some data for select an 
        /// <see cref="Order.State"/> in frontend
        /// by specifying an <paramref name="orderId"/>. Uses by GET-method to pass dto for select 
        /// <see cref="Order.State"/> into View.
        /// </summary>
        /// <param name="orderId">The integer order identifier.</param>
        /// <returns>The <see cref="ChangeOrderStateDto"/> model.</returns>
        public Task<ChangeOrderStateDto> GetOrderState(int orderId);

    }
}
