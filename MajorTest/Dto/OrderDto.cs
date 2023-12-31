using MajorTest.Models;
using System.ComponentModel.DataAnnotations;

namespace MajorTest.Dto
{
    public class OrderDto
    {
        public int? Id { get; set; }

        [Required]
        public Item Item { get; set; } = null!;

        [Required]
        public ItemSender ItemSender { get; set; } = null!;

        public Courier? Courier { get; set; }

        [Required]
        public ItemReceiver ItemReceiver { get; set; } = null!;

        public string? State { get; set; }


        [Required]
        public DateTime MeetingTime { get; set; }
        [Required]
        public string MeetingPlace { get; set; } = null!;
        [Required]
        public string TargetAddress { get; set; } = null!;

        public OrderDto()
        {

        }

        public OrderDto(Item item, ItemSender itemSender, ItemReceiver ir, DateTime meetingTime,
            string meetingPlace, string targetAddress)
        {
            Item = item;
            ItemSender = itemSender;
            ItemReceiver = ir;
            MeetingTime = meetingTime;
            MeetingPlace = meetingPlace;
            TargetAddress = targetAddress;
        }
    }
}
