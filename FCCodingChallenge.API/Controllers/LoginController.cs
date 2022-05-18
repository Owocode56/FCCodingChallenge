using FCCodingChallenge.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FCCodingChallenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : BaseController
    {
        private readonly IUserService _userService;
        public LoginController(IUserService userService, IHttpContextAccessor httpContext, ILoggerManager loggerManager)
         : base(httpContext, loggerManager)
        {
            _userService = userService;
        }


        [HttpGet]
        [SwaggerOperation(Summary = "Authorization")]
        [Route("login")]
        
        public async Task<IActionResult> login(string email , string password)
        {
            var resp = this.CustomResponse(await _userService.Login(email , password));
            return resp;
        }
    }
}
