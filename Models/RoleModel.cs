using System.ComponentModel.DataAnnotations;

namespace Funko_shop.Models;

public class Role
{
  [Key]
  public int id_role { get; set; }
  public required string role { get; set; }
  public int level_access { get; set; }
  public required ICollection<User> UserReference { get; set; }
}