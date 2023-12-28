using MajorTest.Models;

namespace MajorTest.ViewModels
{
    /// <summary>
    /// ViewModel for pagination, to pass info about couriers into View.
    /// </summary>
    public class IndexCourierViewModel
	{
        /// <summary>
        /// All <see cref="Courier"/> objects on target page.
        /// </summary>
		public IEnumerable<Courier> Couriers { get; set; }

        /// <summary>
        /// Incapsulates current <see cref="PageViewModel.PageNumber"/> and <see cref="PageViewModel.TotalPages"/> 
        /// num and properties for check if there are previous and next page: 
        /// <see cref="PageViewModel.HasPreviousPage"/>,
        /// <see cref="PageViewModel.HasNextPage"/>.
        /// </summary>
		public PageViewModel PageViewModel { get; set; }
	}
}
