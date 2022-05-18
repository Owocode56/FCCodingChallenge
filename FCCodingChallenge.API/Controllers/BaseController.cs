using FCCodingChallenge.API.Data;
using FCCodingChallenge.API.Data.Enum;
using FCCodingChallenge.API.Data.Models;
using FCCodingChallenge.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FCCodingChallenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public RemoteDetails _remoteDetails;
        public ILoggerManager _loggerManger;

        public BaseController( IHttpContextAccessor httpContext, ILoggerManager loggerManger)
        {
                _loggerManger = loggerManger;
        }


            protected IActionResult CustomResponse<T>(GenericResponse<T> result)
        {
            ResponseCode.TryParse(result.ResponseCode, out ResponseCode myStatus);
            result.ResponseCode = result.ResponseCode.Length > 1 ? result.ResponseCode : '0' + result.ResponseCode;
            //_loggerManger.Information($"{result.Caller} Response to: {JsonConvert.SerializeObject(_remoteDetails)} Response Body : {JsonConvert.SerializeObject(result)}");
            switch (myStatus)
            {
                case ResponseCode.ProcessingError:
                    return Ok(result);
                case ResponseCode.AuthorizationError:
                    return Ok(result);
                case ResponseCode.NotFound:
                    return Ok(result);
                default:
                    return Ok(result);
            }
        }

      
    }
}
