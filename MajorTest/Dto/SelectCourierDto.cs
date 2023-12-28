using Microsoft.AspNetCore.Mvc.Rendering;
using MajorTest.Models;

namespace MajorTest.Dto
{
    /// <summary>
    /// Data-transfer object to pass info (properties) for select <see cref="Courier"/> into View.
    /// </summary>
    public class SelectCourierDto
	{
        /// <summary>
        /// Incapsulate all <see cref="Courier"/> objects from <see cref="MajorContext.Couriers"/>.
        /// </summary>
        public SelectList? AllCouriers { get; set; } = null!;

        /// <summary>
        /// Selected from <see cref="AllCouriers"/> <see cref="Courier"/> identifier.
        /// </summary>
		public int SelectedCourierId { get; set; }

        /// <summary>
        /// Target <see cref="Order"/> identifier.
        /// </summary>
		public int ThisOrderId { get; set; }
	}
}
