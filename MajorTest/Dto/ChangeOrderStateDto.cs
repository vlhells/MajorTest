using Microsoft.AspNetCore.Mvc.Rendering;

namespace MajorTest.Dto
{
    public class ChangeOrderStateDto
    {
        public SelectList? orderStates { get; set; } = null!;
        public string selectedState { get; set; } = null!;
        public int thisOrderId { get; set; }
        public string? cancellationComment { get; set; }
    }
}
