using MajorTest.Models;

namespace MajorTest.Services.OrderService
{
    public interface IOrderService
    {
		public Task<IEnumerable<Order>> IndexAsync();
		public Task CreateAsync(Order newOrder);
        public Task EditAsync(Order updatedOrder);
        public Task<Order> GetOrderByIdAsync(int id);
        public Task<bool> DeleteAsync(int id);

    }
}
