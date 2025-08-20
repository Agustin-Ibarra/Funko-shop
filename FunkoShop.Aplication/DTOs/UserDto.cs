using System.ComponentModel.DataAnnotations;

namespace FunkoShop.Aplication.DTOs;

public class LoginDto
{
  [Required(ErrorMessage = "El correo es requerido")]
  [EmailAddress(ErrorMessage = "El correo no esta en un formato valido")]
  public required string Email { get; set; }

  [Required(ErrorMessage = "La contraseña es requerida")]
  [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "la contraseña solo puede contener letras y numeros")]
  public string? Password { get; set; }
}

public class RegisterDto
{
  [Required(ErrorMessage = "El nombre es requerido")]
  [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "El nombre debe contener letras")]
  public required string Name { get; set; }

  [Required(ErrorMessage = "El apellido es requerido")]
  [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "El apellido debe contener letras")]

  public required string LastName { get; set; }

  [Required(ErrorMessage = "El correo es requerido")]
  [EmailAddress(ErrorMessage = "El correo no esta en un formato valido")]
  public required string Email { get; set; }

  [Required(ErrorMessage = "La contraseña es requerida")]
  [StringLength(maximumLength: 25, MinimumLength = 8, ErrorMessage = "La contraseña no puede ser menor a 8 caracteres")]
  [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "la contraseña solo puede contener letras y numeros")]
  public required string Password { get; set; }
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