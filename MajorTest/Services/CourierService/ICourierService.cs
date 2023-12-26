using MajorTest.Models;

namespace MajorTest.Services.CourierService
{
    public interface ICourierService
    {
        public Task<IEnumerable<Courier>> IndexAsync(string searchString);
        public Task CreateAsync(Courier newCourier);
        public Task EditAsync(Courier updatedCourier);
        public Task<Courier> GetCourierByIdAsync(int id);
        public Task<bool> DeleteAsync(int id);
    }
}
