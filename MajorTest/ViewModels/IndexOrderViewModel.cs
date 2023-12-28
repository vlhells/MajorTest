using MajorTest.Models;

namespace MajorTest.ViewModels
{
    /// <summary>
    /// ViewModel for pagination, to pass info about orders into View.
    /// </summary>
    public class IndexOrderViewModel
	{
        /// <summary>
        /// All <see cref="Order"/> objects on target page.
        /// </summary>
		public IEnumerable<Order> Orders { get; set; }

        /// <summary>
        /// Incapsulates current <see cref="PageViewModel.PageNumber"/> and <see cref="PageViewModel.TotalPages"/> 
        /// num and properties for check if there are previous and next page: 
        /// <see cref="PageViewModel.HasPreviousPage"/>,
        /// <see cref="PageViewModel.HasNextPage"/>.
        /// </summary>
		public PageViewModel PageViewModel { get; set; }
	}
}
