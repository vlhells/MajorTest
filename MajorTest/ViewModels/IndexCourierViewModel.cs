using MajorTest.Models;

namespace MajorTest.ViewModels
{
	public class IndexCourierViewModel
	{
		public IEnumerable<Courier> Couriers { get; set; }
		public PageViewModel PageViewModel { get; set; }
	}
}
