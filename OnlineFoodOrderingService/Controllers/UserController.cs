using OnlineFoodOrderingService.DTO;
using OnlineFoodOrderingService.DTO.User;
using OnlineFoodOrderingService.IRepository;
using OnlineFoodOrderingService.Manager;
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
        UserManager userManager;
        Response<UserDto> response;

        public UserController(IUserRepository repository)
        {
            userManager = new UserManager(repository);
            response = new Response<UserDto>();
        }

        [Route("SignUp")]
        [HttpPost]
        public Response<UserDto> SignUp(Request<UserDto> request)
        {
            response = userManager.SignUp(request);
            return response;
        }

        [Route("LogIn")]
        [HttpPost]
        public Response<UserDto> LogIn(Request<UserDto> request)
        {
            response = userManager.LogIn(request);
            return response;
        }

		[Route("GetDetails")]
		[HttpPost]
		public Response<UserDto> GetDetails(string UserId)
		{
			response = userManager.GetDetails(UserId);
			return response;
		}
	}
}
