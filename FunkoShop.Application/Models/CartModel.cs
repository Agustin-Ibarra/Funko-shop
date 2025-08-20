using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FunkoShop.Aplication.Models;

public class Cart
{
  [Key]
  public int id_cart { get; set; }
  public int customer { get; set; }
  [ForeignKey("customer")]
  public User? CustomerFk { get; set; }
  public int item { get; set; }
  [ForeignKey("item")]
  public Item? ItemFk { get; set; }
  public int quantity { get; set; }
}