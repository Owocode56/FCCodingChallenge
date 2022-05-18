using FCCodingChallenge.API.Data;
using FCCodingChallenge.API.Data.Models;
using FCCodingChallenge.API.Data.ViewModels;
using FCCodingChallenge.API.Repository;
using FCCodingChallenge.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCCodingChallenge.Tests
{
    public class Helper
    {
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;
        public IConfiguration Configuration { get; }

        public Helper()
        {
            var services = new ServiceCollection();

            var cct = Configuration.GetConnectionString("FCreditConnectionString");

            services.AddDbContext<FCCodingChallenge.API.Data.AppContext>(options =>
               options.UseSqlServer(
                   Constants.ConnectionString, providerOptions => providerOptions.EnableRetryOnFailure()));

            services.AddTransient<IDapperRepository, DapperRepository>();

            services.AddTransient<IUserRoleService, UserRoleService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ILoggerManager, LoggerManager>();

            services.AddScoped<IUserRepository, UserRepository>();
            //services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            //services.AddScoped<IUserRoleService, UserRoleService>();

            services.AddTransient<RemoteDetails, RemoteDetails>();

            services.AddTransient<RemoteDetails, RemoteDetails>();

            var serviceProvider = services.BuildServiceProvider();

            _userService = serviceProvider.GetService<IUserService>();
            _userRoleService = serviceProvider.GetService<IUserRoleService>();
        }

        
        public async Task<GenericResponse<User>> UserModule_CanRetrieveUserWithRole()
        {
            var userObj = new UserVM
            {
                Email = "johntree@gmail.com",
                Firstname = "John",
                Lastname = "Tree",
                Gender = "M",
                DateOfBirth = "1987-10-23"
            };

            Logger.Info("UserModule_CanRetrieveUserWithRole started");
            var response = await _userService.GetUser(Constants.Email);
            Logger.Info($"UserModule_CanRetrieveUserWithRole Response - {JsonConvert.SerializeObject(response)}");
            //Assert.IsTrue(response.IsSuccessful);

            return response;
        }
    }
}
