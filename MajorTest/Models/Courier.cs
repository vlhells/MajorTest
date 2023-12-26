using MajorTest.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace MajorTest.Models
{
	public class Courier: IHuman
	{
		[Key]
		public int Id { get; private set; }

		public string FirstName { get; set; } = null!;
		public string? SecondName { get; set; }
		public string LastName { get; set; } = null!;
	}
}
