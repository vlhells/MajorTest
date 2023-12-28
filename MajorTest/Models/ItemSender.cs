using System.ComponentModel.DataAnnotations;
using MajorTest.Interfaces;

namespace MajorTest.Models
{
	/// <summary>
	/// Class incapsulates all info about <see cref="ItemSender"/>.
	/// </summary>
    public class ItemSender : IHuman
	{
		[Key]
		public int Id { get; private set; }

		public string FirstName { get; set; } = null!;
		public string? SecondName { get; set; }
		public string LastName { get; set; } = null!;

		public string PhoneNumber { get; set;} = null!;
	}
}
