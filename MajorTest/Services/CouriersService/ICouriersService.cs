using MajorTest.Models;
using MajorTest.ViewModels;

namespace MajorTest.Services.CouriersService
{
    public interface ICouriersService
    {
        public Task<IndexCourierViewModel> IndexAsync(string? searchString, int page);
        public Task CreateAsync(Courier newCourier);
        public Task EditAsync(Courier updatedCourier);
        public Task<Courier> GetCourierByIdAsync(int id);
        public Task<bool> DeleteAsync(int id);
    }
}
