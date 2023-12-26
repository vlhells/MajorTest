using System.ComponentModel.DataAnnotations;

namespace MajorTest.Models
{
	public class Item
	{
		[Key]
		public int Id { get; private set; }

		public string Description { get; set; } = null!;
		public float Weight { get; set; }
		public float Width { get; set; }
		public float Height { get; set; }

		public Item(string description, float weight, float width, float height)
		{
			Description = description;
			Weight = weight;
			Width = width;
			Height = height;
		}

		public Item()
		{

		}
	}
}
