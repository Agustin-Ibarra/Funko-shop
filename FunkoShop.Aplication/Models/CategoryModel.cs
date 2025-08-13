using System.ComponentModel.DataAnnotations;

namespace FunkoShop.Aplication.Models;

public class Category
{
  [Key]
  public int id_category { get; set; }
  public string? name_category { get; set; }
  public int disontinued { get; set; }
  public ICollection<Item>? ItemReference { get; set; }
}