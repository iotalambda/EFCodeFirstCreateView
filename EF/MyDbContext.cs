using Microsoft.EntityFrameworkCore;

namespace EFCodeFirstCreateView.EF;
public class MyDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderRow> OrderRows { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source=mydb.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderRow>()
            .HasKey(x => new { x.OrderId, x.ProductId });

        modelBuilder.Entity<OrderRow>()
            .HasOne(x => x.Order)
            .WithMany(x => x.OrderRows)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderRow>()
            .HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
