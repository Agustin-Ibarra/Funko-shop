using System.ComponentModel.DataAnnotations;

namespace Funko_shop.DTOs;

public class LoginDto
{
  [Required(ErrorMessage = "El correo es requerido")]
  [EmailAddress(ErrorMessage = "El correo no esta en un formato valido")]
  public required string Email { get; set; }

  [Required(ErrorMessage = "La contraseña es requerida")]
  [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "la contraseña solo puede contener letras y numeros")]
  public string? Password { get; set; }
}

public class UserCredentialsDto
{
  public int IdUser { get; set; }
  public string? Email { get; set; }
  public string? Password { get; set; }
}

public class ProfileDto
{
  public string? Name { get; set; }
  public string? LastName { get; set; }
  public string? Email { get; set; }
}