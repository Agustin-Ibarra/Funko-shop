using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FunkoShop.Aplication.Models;

public class User
{
  [Key]
  public int id_user { get; set; }
  [Required(ErrorMessage = "El nombre es requerido")]
  [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "El nombre debe contener letras")]
  public string? name { get; set; }
  [Required(ErrorMessage = "El apellido es requerido")]
  [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "El apellido debe contener letras")]
  public string? last_name { get; set; }
  [Required(ErrorMessage = "El correo es requerido")]
  [EmailAddress(ErrorMessage = "El correo no esta en un formato valido")]
  public string? email { get; set; }
  [Required(ErrorMessage = "La contraseña es requerida")]
  [StringLength(maximumLength: 25, MinimumLength = 8, ErrorMessage = "La contraseña no puede ser menor a 8 caracteres")]
  [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "la contraseña solo puede contener letras y numeros")]
  public string? user_password { get; set; }
  public int user_role { get; set; }
  [ForeignKey("user_role")]
  public Role? RoleFk { get; set; }
  public DateTime? create_time { get; set; }
  public ICollection<Cart>? CartReference { get; set; }
}