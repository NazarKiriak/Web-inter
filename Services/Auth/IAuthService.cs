using System.Threading.Tasks;
using RESTwebAPI.Models;

namespace RESTwebAPI.Services
{
    public interface IAuthService
    {
        Task<User> AuthenticateAsync(string email, string password);
        Task<User> RegisterAsync(User user);
    }
}
