using Microsoft.EntityFrameworkCore;

namespace MajorTest.Models
{
	public class MajorContext: DbContext
	{
		public DbSet<Order> Orders { get; private set; } = null!;
		public DbSet<ItemSender> Senders { get; private set; } = null!;
		public DbSet<Courier> Couriers { get; private set; } = null!;
		public DbSet<Item> Items { get; private set; } = null!;
		public DbSet<ItemReceiver> Receivers { get; private set; } = null!;

		public MajorContext(DbContextOptions<MajorContext> options) : base(options)
		{
			//Database.EnsureDeleted();
			Database.EnsureCreated();
		}
	}
}
