using Microsoft.AspNetCore.Mvc.Rendering;
using MajorTest.Models;

namespace MajorTest.Dto
{
    /// <summary>
    /// Data-transfer object to pass info (properties) for select <see cref="Order.State"/> into View.
    /// </summary>
    public class ChangeOrderStateDto
    {
        /// <summary>
        /// Incapsulate necessary (depends on case) <see cref="Order.OrderStates"/>.
        /// </summary>
        public SelectList? OrderStates { get; set; } = null!;

        /// <summary>
        /// New <see cref="Order.State"/>, selects from <see cref="OrderStates"/>.
        /// </summary>
        public string SelectedState { get; set; } = null!;

        /// <summary>
        /// Target <see cref="Order"/> identifier.
        /// </summary>
        public int ThisOrderId { get; set; }

        /// <summary>
        /// <see cref="Order.CancellationComment"/> for case if new <see cref="Order.State"/> 
        /// (<see cref="SelectedState"/>) is "cancelled" (from <see cref="Order.OrderStates"/>).
        /// </summary>
        public string? CancellationComment { get; set; }
    }
}
