using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FunkoShop.Aplication.Models;

public class Item
{
  [Key]
  public int id_item { get; set; }
  public string? code { set; get; }
  public string? name { get; set; }
  public string? description { get; set; }
  public int category { get; set; }
  [ForeignKey("category")]
  public required Category categoryFk { get; set; }
  public double unit_price { get; set; }
  public int stock { get; set; }
  public string? image_1 { get; set; }
  public string? image_2 { get; set; }
  public int supplier { get; set; }
}