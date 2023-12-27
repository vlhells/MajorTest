using Microsoft.AspNetCore.Mvc.Rendering;

namespace MajorTest.Dto
{
	public class SelectCourierDto
	{
		public SelectList? allCouriers { get; set; } = null!;
		public int selectedCourierId { get; set; }
		public int thisOrderId { get; set; }
	}
}
