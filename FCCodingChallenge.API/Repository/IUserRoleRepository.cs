using System.Threading.Tasks;

namespace FCCodingChallenge.API.Repository
{
    public interface IUserRoleRepository
    {
        Task<long> AddUserRole(long userId, int roleId);
        Task<long> DeleteUserRole(long userId);
    }
}