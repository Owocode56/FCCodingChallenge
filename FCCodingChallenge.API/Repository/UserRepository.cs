using Dapper;
using FCCodingChallenge.API.Data.Models;
using FCCodingChallenge.API.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace FCCodingChallenge.API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IDapperRepository _dapperRepository;

        public UserRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }


        public async Task<long> AddUser(User userVM)
        {
            var queryString = @"INSERT INTO [User] OUTPUT inserted.Id VALUES (@Firstname, @Lastname, @Email, @Phone, @Gender, @DateOfBirth, @Role,  @Nationality, @DateCreated, @DateUpdated)";

            var dbPara = new DynamicParameters();
            dbPara.Add("Firstname", userVM.FirstName);
            dbPara.Add("Lastname", userVM.LastName);
            dbPara.Add("Email", userVM.Email);
            dbPara.Add("Phone", userVM.Phone);
            dbPara.Add("Role", userVM.Role);
            dbPara.Add("Gender", userVM.Gender);
            dbPara.Add("DateOfBirth", userVM.DateOfBirth);
            dbPara.Add("Nationality", userVM.Nationality);
            dbPara.Add("DateCreated", DateTime.Now.AddHours(1));
            dbPara.Add("DateUpdated", null);

            var response = await _dapperRepository.Insert<long>(queryString, dbPara, commandType: CommandType.Text);

            return response;
        }

        public async Task<User> GetUser(long userID)
        {
            var queryString = @"Select * from [User] where ID = @UserID";
            var dbPara = new DynamicParameters();
            dbPara.Add("UserID", userID);

            var response = await _dapperRepository.Get<User>(queryString, dbPara, commandType: CommandType.Text);

            return response;
        }

        public async Task<List<User>> GetUsers()
        {

            var queryString = @"Select * from [User]";
            var dbPara = new DynamicParameters();
            var response = await _dapperRepository.GetAll<User>(queryString, dbPara, commandType: CommandType.Text);

            return response;
        }

        public async Task<long> UpdateUser(User user)
        {
            var queryString = @"UPDATE [User]  set Firstname=@Firstname, Lastname=@Lastname, Email=@Email, Phone=@Phone, 
                                Gender=@Gender, DateOfBirth=@DateOfBirth, Nationality=@Nationality, DateUpdated=@DateUpdated WHERE Id=@Id";

            var dbPara = new DynamicParameters();
            dbPara.Add("Id", user.Id);
            dbPara.Add("Firstname", user.FirstName);
            dbPara.Add("Lastname", user.LastName);
            dbPara.Add("Email", user.Email);
            dbPara.Add("Phone", user.Phone);
            dbPara.Add("Gender", user.Gender);
            dbPara.Add("DateOfBirth", user.DateOfBirth);
            dbPara.Add("Nationality", user.Nationality);
            dbPara.Add("DateUpdated", DateTime.Now.AddHours(1));

            var response = await _dapperRepository.Execute<long>(queryString, dbPara, commandType: CommandType.Text);

            return response;

        }

        public async Task<long> DeleteUser(long userID)
        {
            var queryString = @"Delete [user] where ID = @UserID";

            var dbPara = new DynamicParameters();
            dbPara.Add("UserID", userID);

            var response = await _dapperRepository.Execute<long>(queryString, dbPara, commandType: CommandType.Text);

            return response;
        }

        public async Task<User> Login(string email , string password)
        {
            var queryString = @"Select * from [User] where email = @Email and password = @Password";

            var dbPara = new DynamicParameters();
            dbPara.Add("Email", email);
            dbPara.Add("Password", password);
            var response = await _dapperRepository.Get<User>(queryString, dbPara, commandType: CommandType.Text);

            return response;
        }

    }
}
