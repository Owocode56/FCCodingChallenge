using FCCodingChallenge.API.Data.Models;
using FCCodingChallenge.API.Data.ViewModels;
using FCCodingChallenge.API.Services;
using FCCodingChallenge.API.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FCCodingChallenge.API.Controllers
{
    [Route("api/UserManagement")]
    [ApiController]
    public class UserController : BaseController
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService, IHttpContextAccessor httpContext, ILoggerManager loggerManager)
          : base( httpContext, loggerManager)
        {
            _userService = userService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get User")]
        [Route("get-userbyID/{userID}")]
        
        public async Task<IActionResult> GetUser(long userID)
        {
            var resp = this.CustomResponse(await _userService.GetUser(userID));
            return resp;
        }



        [HttpPost]
        [SwaggerOperation(Summary = "Create New User and Assign Role")]
        [Route("create-user")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddUser(UserVM userRoleRequest)
        {
            var resp = this.CustomResponse(await _userService.AddUser(_remoteDetails, userRoleRequest));
            return resp;
        }

       

        [HttpPut]
        [SwaggerOperation(Summary = "Update User Profile")]
        [Route("update-user")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUser(UpdateUserRequest userVM)
        {
            var resp = this.CustomResponse(await _userService.UpdateUser(_remoteDetails, userVM));
            return resp;
        }

        [HttpDelete]
        [Route("delete-user/{userID}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long userID)
        {
            var resp = this.CustomResponse(await _userService.DeleteUser(userID));
            return resp;
        }

        [HttpDelete]
        [Route("delete-users")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(List<long> userIDList)
        {
            var resp = this.CustomResponse(await _userService.DeleteUser(userIDList));
            return resp;
        }
    }
}
