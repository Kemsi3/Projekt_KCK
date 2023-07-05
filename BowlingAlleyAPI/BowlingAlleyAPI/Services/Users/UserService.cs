using BowlingAlleyAPI.Data;
using BowlingAlleyAPI.Models;
using System.Runtime.CompilerServices;

namespace BowlingAlleyAPI.Services.Users
{
    public class UserService : IUserService
    {

        public readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public async Task<IResult> Login(string email, string password)
        {
            User userFound = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

            if(userFound == null) {
                return Results.NotFound("Bad login or password");
            }

            return Results.Ok(userFound);

        }

    }
}
