using Microsoft.EntityFrameworkCore;

namespace MajorTest.Models
{
    /// <summary>
    /// Db-class.
    /// </summary>
    public class MajorContext : DbContext
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
            .HasOne(o => o.ItemSender)
            .WithOne()
            .HasForeignKey<ItemSender>(i => i.Id)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
            .HasOne(o => o.Item)
            .WithOne()
            .HasForeignKey<Item>(i => i.Id)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
            .HasOne(o => o.ItemReceiver)
            .WithOne()
            .HasForeignKey<ItemReceiver>(i => i.Id)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
