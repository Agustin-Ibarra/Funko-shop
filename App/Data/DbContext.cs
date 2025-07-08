using Microsoft.EntityFrameworkCore;
using Funko_shop.Models;

namespace Funko_shop.Data;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
  public DbSet<User> Users { get; set; }
  public DbSet<Item> Items { get; set; }
  public DbSet<Category> Categories { get; set; }
  public DbSet<Role> Roles { get; set; }
  public DbSet<Cart> Carts { get; set; }
  public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
  public DbSet<PurchaseDetail> PurchaseDetails { get; set; }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<User>().ToTable("users");
    modelBuilder.Entity<Category>().ToTable("categories");
    modelBuilder.Entity<Role>().ToTable("user_roles");
    modelBuilder.Entity<Cart>().ToTable("carts");
    modelBuilder.Entity<PurchaseOrder>().ToTable("purchases");
    modelBuilder.Entity<PurchaseDetail>().ToTable("purchase_details");
  }
}