using Common.Helpers;
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
using System.Web.Http.Cors;

namespace OnlineFoodOrderingService.Controllers
{
	[EnableCors(origins: "*", headers: "*", methods: "*")]
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

		[Route("GetUsersList")]
		[HttpPost]
		public Response<UserDto> GetUsersList(Request<UserDto> request)
		{
			response = userManager.GetUsersList(request);
			return response;
		}

		[Route("GetDetails")]
		[HttpPost]
		public Response<UserDto> GetDetails(string UserId)
		{
			response = userManager.GetDetails(UserId);
			return response;
		}

		[Route("UpdateDetails")]
		[HttpPost]
		public Response<UserDto> UpdateDetails(Request<UserDto> request)
		{
			response = userManager.UpdateProfile(request);
			return response;
		}

		[Route("SendMail")]
		[HttpPost]
		public Response<UserDto> SendMail(Request<UserDto> request)
		{
			EMailHelper mailHelper = new EMailHelper(EMailHelper.EMAIL_SENDER, EMailHelper.EMAIL_CREDENTIALS, EMailHelper.SMTP_CLIENT);
			var emailBody = String.Format(EMailHelper.EMAIL_BODY);
			response = mailHelper.SendEMail("shammyk123@gmail.com", EMailHelper.EMAIL_SUBJECT, emailBody);
			return response; 
		}
	}
}
