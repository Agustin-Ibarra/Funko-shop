using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FunkoShop.Aplication.Models;

public class PurchaseOrder
{
  [Key]
  public int id_purchase { get; set; }
  public DateTime date_purchase { get; set; }
  public string? seller { get; set; }
  public int customer { get; set; }
  [ForeignKey("customer")]
  public User? CustomerFk { get; set; }
  public double total { get; set; }
  public ICollection<PurchaseDetail>? PurchaseDetailReference { get; set; }
  public PurchaseOrder(DateTime date_purchase, string seller, int customer, double total)
  {
    this.date_purchase = date_purchase;
    this.seller = seller;
    this.customer = customer;
    this.total = total;
  }
  public PurchaseOrder(){}
}