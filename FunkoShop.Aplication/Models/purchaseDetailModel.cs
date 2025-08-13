using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FunkoShop.Aplication.Models;

public class PurchaseDetail
{
  [Key]
  public int id_purchase_detail { get; set; }
  public int id_purchase_order { get; set; }
  [ForeignKey("id_purchase_order")]
  public required PurchaseOrder PurchaseOrderFk { get; set; }
  public int item { get; set; }
  [ForeignKey("item")]
  public Item? itemNameFk { get; set; }
  public int item_price { get; set; }
  [ForeignKey("item_price")]
  public Item? ItemPriceFk { get; set; }
  public int quantity { get; set; }
  public double subtotal { get; set; }
}