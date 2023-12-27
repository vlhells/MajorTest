using MajorTest.Models;

namespace MajorTest.Services.CouriersService
{
    public interface ICouriersService
    {
        public Task<IEnumerable<Courier>> IndexAsync(string searchString);
        public Task CreateAsync(Courier newCourier);
        public Task EditAsync(Courier updatedCourier);
        public Task<Courier> GetCourierByIdAsync(int id);
        public Task<bool> DeleteAsync(int id);
    }
}
