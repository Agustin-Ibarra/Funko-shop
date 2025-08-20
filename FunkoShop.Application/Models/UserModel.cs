using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FunkoShop.Aplication.Models;

public class User
{
  [Key]
  public int id_user { get; set; }
  public required string name { get; set; }
  public required string last_name { get; set; }
  public required string email { get; set; }
  public required string user_password { get; set; }
  public int user_role { get; set; }
  [ForeignKey("user_role")]
  public Role? RoleFk { get; set; }
  public DateTime create_time { get; set; }
  public ICollection<Cart>? CartReference { get; set; }
}