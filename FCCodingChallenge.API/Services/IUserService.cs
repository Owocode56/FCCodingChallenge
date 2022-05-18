using FCCodingChallenge.API.Data;
using FCCodingChallenge.API.Data.Models;
using FCCodingChallenge.API.Data.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FCCodingChallenge.API.Services
{
    public interface IUserService
    {
        Task<GenericResponse<object>> AddUser(RemoteDetails remoteDetails, UserVM request);
        Task<GenericResponse<User>> GetUser(long userID);
        Task<GenericResponse<object>> UpdateUser(RemoteDetails remoteDetails, UpdateUserRequest request);
        Task<GenericResponse<object>> DeleteUser(long userID);
        Task<GenericResponse<object>> DeleteUser(List<long> userIDList);
        Task<GenericResponse<object>> Login(string userName, string password);
    }
}
