using FCCodingChallenge.API;
using FCCodingChallenge.API.Data.ViewModels;
using FCCodingChallenge.API.Repository;
using FCCodingChallenge.API.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCCodingChallenge.Tests
{
    [TestClass]
    public class UserModuleTests
    {
        //private readonly IUserService _userService;
        //private readonly IUserRoleService _userRoleService;
        private readonly Helper helper;

      public UserModuleTests()
        {
            helper = new Helper();
        }

        [TestMethod]
        public void CanRetrieveUserWithRole()
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
            //var response = await helper.UserModule_CanRetrieveUserWithRole();
            ////var response = await _userService.GetUser(Constants.Email);
            //Logger.Info($"UserModule_CanRetrieveUserWithRole Response - {JsonConvert.SerializeObject(response)}");
            //Assert.IsTrue(response.IsSuccessful);
        }

        //public async void UserModule_CannotRetrieveUserWithRole()
        //{
        //    var userObj = new UserVM
        //    {

        //    };

        //    var response = await _userService.GetUser("");
        //    Assert.IsTrue(response.IsSuccessful);
        //}
    }
}
