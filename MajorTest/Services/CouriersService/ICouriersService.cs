using MajorTest.Models;
using MajorTest.ViewModels;

namespace MajorTest.Services.CouriersService
{
    /// <summary>
    /// The interface determines service for work with <see cref="Courier"/> objects.
    /// </summary>
    public interface ICouriersService
    {
        /// <summary>
        /// Returns a <see cref="IndexCourierViewModel"/> object with the content (info about couriers)
        /// on target page (pagination)
        /// by specifying a <paramref name="searchString"/> and <paramref name="page"/>.
        /// </summary>
        /// <param name="searchString">String (key-word) for search content (checks Full-name columns
        /// and phone numbers).</param>
        /// <param name="page">Num of target page in pagination.</param>
        /// <returns>An <see cref="IndexCourierViewModel"/> object.</returns>
        public Task<IndexCourierViewModel> IndexAsync(string? searchString, int page);

        /// <summary>
        /// Creates an <see cref="Courier"/> object in <see cref="MajorContext.Couriers"/> 
        /// via Entity Framework Core abilities with info
        /// from frontend (GET-method Create): <paramref name="newCourier"/>.
        /// </summary>
        /// <param name="newCourier">Courier object got from GET-method Create (frontend).</param>
        public Task CreateAsync(Courier newCourier);

        /// <summary>
        /// Updates existing <see cref="Courier"/> object in <see cref="MajorContext.Couriers"/> 
        /// via Entity Framework Core abilities with new data
        /// from frontend (GET-method Create): <paramref name="updatedCourier"/>.
        /// </summary>
        /// <param name="updatedCourier">Updated Courier object got from GET-method Edit (frontend).</param>
        public Task EditAsync(Courier updatedCourier);

        /// <summary>
        /// Returns a <see cref="Courier"/> object with its' internal content 
        /// by specifying a <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The integer courier identifier.</param>
        /// <returns>An <see cref="Courier"/> object.</returns>
        public Task<Courier> GetCourierByIdAsync(int id);

        /// <summary>
        /// Returns a <see cref="bool"/> value by specifying an <paramref name="id"/>.
        /// <see cref="true"/> if <see cref="Courier"/> object with this <paramref name="id"/>
        /// has been sucessfully deleted from <see cref="MajorContext.Couriers"/>, otherwise, <see cref="false"/>.
        /// </summary>
        /// <param name="id">The integer courier identifier.</param>
        /// <returns>The <see cref="bool"/> value.</returns>
        public Task<bool> DeleteAsync(int id);
    }
}
