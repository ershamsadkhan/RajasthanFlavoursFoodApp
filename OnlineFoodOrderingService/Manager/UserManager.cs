using OnlineFoodOrderingService.DTO;
using OnlineFoodOrderingService.DTO.User;
using OnlineFoodOrderingService.IRepository;
using System.Web;
using System;
using System.Collections.Generic;
using System.Linq;


namespace OnlineFoodOrderingService.Manager
{
	public class UserManager
	{
		Response<UserDto> response;
		IUserRepository repository;


		public UserManager(IUserRepository repository)
		{
			this.repository = repository;
			response = new Response<UserDto>();
		}


		//public methods
		#region 
		public Response<UserDto> SignUp(Request<UserDto> request)
		{
			response = ValidateUser(request);
			if (response.Status == true)
			{
				response = repository.SignUp(request);
			}
			return response;
		}

		public Response<UserDto> GetDetails(string UserId)
		{
			response = repository.GetUserDetails(UserId);
			return response;
		}

		#endregion

		#region 
		public Response<UserDto> LogIn(Request<UserDto> request)
		{
			response = repository.GetLogInDetails(request);
			return response;
		}

		public Response<UserDto> GetUsersList(Request<UserDto> request)
		{
			response = repository.GetUsersList(request);
			return response;
		}

		public Response<UserDto> UpdateProfile(Request<UserDto> request)
		{
			response = repository.UpdateProfile(request);
			return response;
		}

		#endregion
		// private methods

		#region 
		private Response<UserDto> ValidateUser(Request<UserDto> request)
		{
			if (request.Obj.UserName.Trim() == "" || request.Obj.UserPwd.Trim() == "")
			{
				response.Status = false;
				response.ErrMsg = "User Name Or PassWord Cannot Be Blank.";
				return response;
			}

			response = repository.GetUserDetailsFromUserName(request);
			if (response.Status == true)
			{
				response.Status = false;
				response.ErrMsg = "Invalid UserName And Password.";

			}
			else
			{
				response.Status = true;
				response.ErrMsg = "";
			}
			return response;

		}
		#endregion

	}
}