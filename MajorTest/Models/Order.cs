using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MajorTest.Models
{
	/// <summary>
	/// Class incapsulates all info about <see cref="Order"/>.
	/// </summary>
	public class Order
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public Item Item { get; set; } = null!;

        [Required]
        public ItemSender ItemSender { get; set; } = null!;

		public Courier? Courier { get; set; }

        [Required]
        public ItemReceiver ItemReceiver { get; set; } = null!;

		public string State { get; set; } = OrderStates["new"];

		public DateTime MeetingTime { get; set; }
		public string MeetingPlace { get; set; } = null!;
		public string TargetAddress { get; set; } = null!;
		public string? CancellationComment { get; set; }

		[NotMapped]
		public static Dictionary<string, string> OrderStates = new Dictionary<string, string>()
		{
			{ "new", "Новая"},
			{ "inProcess", "Передана на выполнение"},
			{ "cancelled", "Отменена"},
			{ "done", "Выполнена"}
		};
	}
}
