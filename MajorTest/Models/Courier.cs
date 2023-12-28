using MajorTest.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace MajorTest.Models
{
    /// <summary>
    /// Class incapsulates all info about <see cref="Courier"/>.
    /// </summary>
    public class Courier: IHuman
	{
		[Key]
		public int Id { get; set; }

		public string FirstName { get; set; } = null!;
		public string? SecondName { get; set; }
		public string LastName { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;
    }
}
