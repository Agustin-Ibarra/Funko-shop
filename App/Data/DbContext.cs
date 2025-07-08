using Microsoft.EntityFrameworkCore;
using Funko_shop.Models;

namespace Funko_shop.Data;

public class AppDbContext : DbContext // AAppDbContext hereda de DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
  {
    // Ese fragmento define el constructor de la clase AppDbContext
    // se utiliza para inyectar las opciones de configuración del contexto 
    // (como la cadena de conexión o el proveedor de base de datos) desde el exterior, 
    // normalmente a través del sistema de inyección de dependencias de ASP.NET Core.
    // Configura que la entidad Usuario se mapee a la tabla 'usuarios' en la base de datos.
  }
  public DbSet<User> Users { get; set; }
  // Representa la tabla 'Users' en la base de datos
  // permite hacer consultas CRUD sobre entidades UserModel.
  // <UserModel> indica que el DbSet trabajará con entidades del tipo UserModel
  // que representa una tabla.
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
    // Ese método se sobrescribe para personalizar el mapeo entre las clases del modelo y las tablas de la base de datos.
    // En este caso, le estás diciendo a Entity Framework que la clase User se corresponde con una tabla llamada "usuarios" en la base de datos, 
    // en lugar de usar el nombre por defecto (Users). 
    // Esto es útil para adaptar el modelo a una base de datos existente o definir nombres específicos.
  }
}