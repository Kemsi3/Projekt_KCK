using BowlingAlleyAPI.Models;

namespace BowlingAlleyAPI.Services.Users
{
    public interface IUserService
    {
        Task<IResult> Login(string email, string password);
    }
}
