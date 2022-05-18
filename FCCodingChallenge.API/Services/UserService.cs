using FCCodingChallenge.API.Data;
using FCCodingChallenge.API.Data.Enum;
using FCCodingChallenge.API.Data.Models;
using FCCodingChallenge.API.Data.ViewModels;
using FCCodingChallenge.API.Repository;
using FCCodingChallenge.API.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FCCodingChallenge.API.Services
{
    public class UserService : IUserService
    {
        private readonly ILoggerManager _loggerManager;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        public UserService(ILoggerManager loggerManager, IUserRepository userRepository , IConfiguration config) 
        {
            _config = config;
            _loggerManager = loggerManager;
            _userRepository = userRepository;
        }


        public async Task<GenericResponse<object>> AddUser(RemoteDetails remoteDetails, UserVM request)
        {
            string serviceName = "AddUser";
            try
            {
                _loggerManager.Information($"Request from :{JsonConvert.SerializeObject(remoteDetails)} Body:{JsonConvert.SerializeObject(request)}");

                if (string.IsNullOrEmpty(request.FirstName) || string.IsNullOrEmpty(request.LastName) || string.IsNullOrEmpty(request.Email))
                    return new GenericResponse<object> { IsSuccessful = false, ResponseCode = ((int)ResponseCode.ValidationError).ToString(), ResponseMessage = $"Invalid request", Caller = serviceName };

                var password = "";
                var user = new User
                {
                    DateCreated = DateTime.Now,
                    DateOfBirth = request.DateOfBirth,
                    Email = request.Email,
                    FirstName = request.FirstName,
                    Gender = request.Gender,
                    LastName = request.LastName,
                    Nationality = request.Nationality,
                    Role = request.Role,
                    Password = password
                };
                var userId = await _userRepository.AddUser(user);
                if (userId <= 0)
                    return new GenericResponse<object> { IsSuccessful = false, ResponseCode = ((int)ResponseCode.NotFound).ToString(), ResponseMessage = $"User Update Failed", Caller = serviceName };


                return new GenericResponse<object> { IsSuccessful = true, ResponseCode = ((int)ResponseCode.Successful).ToString(), ResponseMessage = $"Success", Caller = serviceName, Data = null };
            }
            catch (Exception ex)
            {
                _loggerManager.Error($"Error {serviceName} - Updating user details", ex);
                return new GenericResponse<object> { IsSuccessful = false, ResponseCode = ((int)ResponseCode.ProcessingError).ToString(), ResponseMessage = $"Processing Error - {ex.Message}", Caller = serviceName };
            }
        }
        public async Task<GenericResponse<User>> GetUser(long userID)
        {
            string serviceName = "GetUser";
            try
            {
                if (userID <= 0)
                    return new GenericResponse<User> { IsSuccessful = false, ResponseCode = ((int)ResponseCode.ValidationError).ToString(), ResponseMessage = $"User ID is required", Caller = serviceName };

                var user = await _userRepository.GetUser(userID);
                if (user == null)
                    return new GenericResponse<User> { IsSuccessful = false, ResponseCode = ((int)ResponseCode.NotFound).ToString(), ResponseMessage = $"No User Found", Caller = serviceName };


                return new GenericResponse<User> { IsSuccessful = true, ResponseCode = ((int)ResponseCode.Successful).ToString(), ResponseMessage = $"Success", Caller = serviceName, Data = user };
            }
            catch (Exception ex)
            {
                _loggerManager.Error($"Error {serviceName} - ", ex);
                return new GenericResponse<User> { IsSuccessful = false, ResponseCode = ((int)ResponseCode.ProcessingError).ToString(), ResponseMessage = $"Processing Error - {ex.Message}", Caller = serviceName };
            }
        }
        public async Task<GenericResponse<object>> UpdateUser(RemoteDetails remoteDetails, UpdateUserRequest request)
        {
            string serviceName = "UpdateUser";
            try
            {
                _loggerManager.Information($"Request from :{JsonConvert.SerializeObject(remoteDetails)} Body:{JsonConvert.SerializeObject(request)}");

                if (string.IsNullOrEmpty(request.FirstName) || string.IsNullOrEmpty(request.LastName) || string.IsNullOrEmpty(request.Email))
                    return new GenericResponse<object> { IsSuccessful = false, ResponseCode = ((int)ResponseCode.ValidationError).ToString(), ResponseMessage = $"Invalid request", Caller = serviceName };

                var user = await GetUser(request.ID);
                if (user.Data is null)
                    return new GenericResponse<object> { IsSuccessful = false, ResponseCode = ((int)ResponseCode.NotFound).ToString(), ResponseMessage = $"No User Found", Caller = serviceName };

                var userDetails = user.Data;
                userDetails.FirstName = request.FirstName;
                userDetails.LastName = request.LastName;
                userDetails.Nationality = request.Nationality;
                userDetails.Phone = request.Phone;
                userDetails.Gender = request.Gender;
                var userId = await _userRepository.UpdateUser(userDetails);
                if (userId <= 0)
                    return new GenericResponse<object> { IsSuccessful = false, ResponseCode = ((int)ResponseCode.NotFound).ToString(), ResponseMessage = $"User Update Failed", Caller = serviceName };

                return new GenericResponse<object> { IsSuccessful = true, ResponseCode = ((int)ResponseCode.Successful).ToString(), ResponseMessage = $"Success", Caller = serviceName, Data = null };
            }
            catch (Exception ex)
            {
                _loggerManager.Error($"Error {serviceName} - Updating user details", ex);
                return new GenericResponse<object> { IsSuccessful = false, ResponseCode = ((int)ResponseCode.ProcessingError).ToString(), ResponseMessage = $"Processing Error - {ex.Message}", Caller = serviceName };
            }
        }
        public async Task<GenericResponse<object>> DeleteUser(long userID)
        {
            try
            {
                if (userID >= 0)
                    return new GenericResponse<object> { IsSuccessful = false, ResponseCode = ((int)ResponseCode.ValidationError).ToString(), ResponseMessage = $"User ID is required" };

                var userDetails = _userRepository.GetUser(userID);

                if (userDetails == null)
                    return new GenericResponse<object> { IsSuccessful = false, ResponseCode = ((int)ResponseCode.NotFound).ToString(), ResponseMessage = $"User not found" };

                await _userRepository.DeleteUser(userID);

                return new GenericResponse<object> { IsSuccessful = true, ResponseCode = ((int)ResponseCode.Successful).ToString(), ResponseMessage = $"User deleted", Data = userDetails };

            }
            catch (Exception ex)
            {
                _loggerManager.Error($"Error - Deleting user details", ex);
                return new GenericResponse<object> { IsSuccessful = false, ResponseCode = ((int)ResponseCode.ProcessingError).ToString(), ResponseMessage = $"Processing Error - {ex.Message}" };
            }
        }


        public async Task<GenericResponse<object>> DeleteUser(List<long> userIDList)
        {
            try
            {
                List<long> successfullyDeletedUsers = null;
                List<long> failedDeletedUsers = null;
                if (userIDList.Count == 0)
                    return new GenericResponse<object> { IsSuccessful = false, ResponseCode = ((int)ResponseCode.ValidationError).ToString(), ResponseMessage = $"No users selected" };

                foreach(var userID in userIDList)
                {
                    try
                    {
                        var userDetails = _userRepository.GetUser(userID);

                        if (userDetails == null)
                        {
                            failedDeletedUsers.Add(userID);
                            continue;
                        }

                        await _userRepository.DeleteUser(userID);
                        successfullyDeletedUsers.Add(userID);
                    }catch(Exception ex)
                    {
                        _loggerManager.Error($"Error - Deleting user details", ex);
                        failedDeletedUsers.Add(userID);
                    }
                }
                var respObject = new { DeletedUser = successfullyDeletedUsers, failedUsers = failedDeletedUsers };
                return new GenericResponse<object> { IsSuccessful = false, ResponseCode = ((int)ResponseCode.Successful).ToString(), ResponseMessage = $"Deleted successfully", Data = respObject };

            }
            catch (Exception ex)
            {
                _loggerManager.Error($"Error - Deleting user details", ex);
                return new GenericResponse<object> { IsSuccessful = false, ResponseCode = ((int)ResponseCode.ProcessingError).ToString(), ResponseMessage = $"Processing Error - {ex.Message}"};
            }
        }
        public async Task<GenericResponse<object>> Login(string userName , string password)
        {
            string Key = _config["Jwt:Key"];
            string issuer = _config["Jwt:Issuer"];
            string audience = _config["Jwt:Audience"];
            try
            {
                if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                    return new GenericResponse<object> { IsSuccessful = false, ResponseCode = ((int)ResponseCode.ValidationError).ToString(), ResponseMessage = $"Username and password are required" };

                var userDetails = await _userRepository.Login(userName , password);

                if (userDetails == null)
                    return new GenericResponse<object> { IsSuccessful = false, ResponseCode = ((int)ResponseCode.NotFound).ToString(), ResponseMessage = $"Invalid username or password" };

     
                SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(Key));
                SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);
                List<Claim> claims = new()
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, userDetails.Email),
                    new Claim("FirstName", userDetails.FirstName),
                    new Claim("Role", userDetails.Role),
                    new Claim("UserName", userDetails.Email)
                };

                JwtSecurityToken token = new(
                issuer: issuer,
                audience: audience,
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);

                string encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

                return new GenericResponse<object> { IsSuccessful = true, ResponseCode = ((int)ResponseCode.Successful).ToString(), ResponseMessage = $"Login successful", Data = encodedToken };

            }
            catch (Exception ex)
            {
                _loggerManager.Error($"Error - Deleting user details", ex);
                return new GenericResponse<object> { IsSuccessful = false, ResponseCode = ((int)ResponseCode.ProcessingError).ToString(), ResponseMessage = $"Processing Error - {ex.Message}"};
            }
        }
    }
}
