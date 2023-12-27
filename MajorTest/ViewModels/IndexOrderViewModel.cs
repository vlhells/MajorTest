using MajorTest.Models;

namespace MajorTest.ViewModels
{
	public class IndexOrderViewModel
	{
		public IEnumerable<Order> Orders { get; set; }
		public PageViewModel PageViewModel { get; set; }
	}
}
