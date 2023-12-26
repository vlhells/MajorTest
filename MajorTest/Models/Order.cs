using System.ComponentModel.DataAnnotations;

namespace MajorTest.Models
{
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

		public string Status { get; set; } = "Новая"; // TODO: think.

		public DateTime MeetingTime { get; set; }
		public string MeetingPlace { get; set; } = null!;
		public string TargetAddress { get; set; } = null!;
		public string? CancellationComment { get; set; }
	}
}
