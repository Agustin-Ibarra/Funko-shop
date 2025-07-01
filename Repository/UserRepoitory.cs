using Funko_shop.Data;
using Funko_shop.DTOs;
using Funko_shop.Models;
using Microsoft.EntityFrameworkCore;

namespace Funko_shop.Repository;

public class UserRepository
{
  private readonly AppDbContext _context;
  // es una referencia al contexto de base de datos (AppDbContext) que definiste heredando de DbContext.
  // Se usa dentro del repositorio para acceder a las tablas (DbSets) y hacer consultas o cambios en la base de datos. Además,
  // al ser readonly se asegura que solo se asigne en el constructor y no cambie después.

  public UserRepository(AppDbContext context)
  {
    // Constructor parametro del tipo de dato AppDbContext
    _context = context;
  }

  public async Task<UserCredentialsDto?> GetUser(string email)
  {
    var userData = await _context.Users
    .Where(user => user.email == email)
    .Select(user => new UserCredentialsDto { IdUser = user.id_user, Email = user.email, Password = user.user_password })
    .FirstOrDefaultAsync();
    return userData;
  }

  public async Task CreateUser(User user)
  {
    _context.Users.Add(user);
    await _context.SaveChangesAsync();
  }

  public async Task<Role> GetRole()
  {
    var roleData = await _context.Roles
    .Where(role => role.id_role == 1)
    .FirstOrDefaultAsync() ?? throw new Exception("No se encontro informacion de la tabla user_role");
    return roleData;
  }

  public async Task<ProfileDto> GetProfile(int id)
  {
    var user = await _context.Users
    .Where(user => user.id_user == id)
    .Select(user => new ProfileDto { Name = user.name, LastName = user.last_name, Email = user.email })
    .FirstOrDefaultAsync() ?? throw new Exception($"No se encontro informacion del usuario con id: ${id}");
    return user;
  }

  public async Task<User> GetUserModel(int id)
  {
    var result = await _context.Users
    .Where(user => user.id_user == id)
    .FirstOrDefaultAsync() ?? throw new Exception($"No se encontro el usuario con id: {id}");
    return result;
  }
}