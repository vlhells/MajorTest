using MajorTest.Models;
using Microsoft.EntityFrameworkCore;

namespace MajorTest.Services.CourierService
{
    public class CourierService: ICourierService
    {
        private MajorContext _db;

        public CourierService(MajorContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Courier>> IndexAsync(string searchString)
        {
            IQueryable<Courier> couriers = _db.Couriers.AsNoTracking();

            if (!String.IsNullOrEmpty(searchString))
            {
                couriers = couriers.Where(o => o.FirstName.Contains(searchString) ||
                                      o.SecondName.Contains(searchString) ||
                                      o.LastName.Contains(searchString) );
            }

            return await couriers.ToListAsync();
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
