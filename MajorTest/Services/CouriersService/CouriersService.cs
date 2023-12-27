using MajorTest.Models;
using MajorTest.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace MajorTest.Services.CouriersService
{
    public class CouriersService: ICouriersService
    {
        private MajorContext _db;

        public CouriersService(MajorContext db)
        {
            _db = db;
        }

        public async Task<IndexCourierViewModel> IndexAsync(string? searchString, int page)
        {
            int pageSize = 1;

            IQueryable<Courier> couriers = _db.Couriers.AsNoTracking();

			if (!String.IsNullOrEmpty(searchString))
            {
                couriers = couriers.Where(o => o.FirstName.Contains(searchString) ||
                                      o.SecondName.Contains(searchString) ||
                                      o.LastName.Contains(searchString) );
            }

			var count = await couriers.CountAsync();
			var items = await couriers.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

			PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexCourierViewModel indexCourierViewModel = new IndexCourierViewModel
			{
                PageViewModel = pageViewModel,
                Couriers = items
			};

			return indexCourierViewModel;
        }

        public async Task CreateAsync(Courier newCourier)
        {
            await _db.Couriers.AddAsync(newCourier);
            await _db.SaveChangesAsync();
        }

        public async Task EditAsync(Courier updatedCourier)
        {
            _db.Couriers.Update(updatedCourier);
            await _db.SaveChangesAsync();
        }

        public async Task<Courier> GetCourierByIdAsync(int id)
        {
            return await _db.Couriers.FindAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var targetCourier = await _db.Couriers.FindAsync(id);
            if (targetCourier != null)
            {
                _db.Couriers.Remove(targetCourier);
                await _db.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
