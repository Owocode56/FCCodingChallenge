using FCCodingChallenge.API.Data.Models;
using FCCodingChallenge.API.Data.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FCCodingChallenge.API.Repository
{
    public interface IUserRepository
    {
        Task<long> AddUser(User user);
        Task<long> DeleteUser(long userID);
        Task<User> GetUser(long userID);
        Task<List<User>> GetUsers();
        Task<long> UpdateUser(User user);
        Task<User> Login(string email, string password);
    }
}
