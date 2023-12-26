using System.ComponentModel.DataAnnotations;
using MajorTest.Interfaces;

namespace MajorTest.Models
{
    public class ItemReceiver: IHuman
	{
		[Key]
		public int Id { get; private set; }

		public string FirstName { get; set; } = null!;
		public string? SecondName { get; set; }
		public string LastName { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;
    }
}
