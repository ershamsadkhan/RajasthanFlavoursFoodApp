using OnlineFoodOrderingService.DTO;
using OnlineFoodOrderingService.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineFoodOrderingService.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        [Route("SignUp")]
        [HttpPost]
        public Response<UserDto> SignUp(Request<UserDto> request)
        {
            Response<UserDto> response = new Response<UserDto>();
            return response;
        }

        [Route("LogIn")]
        [HttpPost]
        public Response<UserDto> LogIn(Request<UserDto> request)
        {
            Response<UserDto> response = new Response<UserDto>();
            return response;
        }
    }
}
