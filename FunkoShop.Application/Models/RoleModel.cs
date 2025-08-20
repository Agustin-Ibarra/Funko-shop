using System.ComponentModel.DataAnnotations;

namespace FunkoShop.Aplication.Models;

public class Role
{
  [Key]
  public int id_role { get; set; }
  public required string role { get; set; }
  public int level_access { get; set; }
  public ICollection<User>? UserReference { get; set; }
}