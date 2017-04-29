using OnlineFoodOrderingService.DTO;
using OnlineFoodOrderingService.DTO.User;
using OnlineFoodOrderingService.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        #region public methods
        public Response<UserDto> SignUp(Request<UserDto> request)
        {
            response = ValidateUser(request);
            if (response.Status == true)
            {
                response=repository.SignUp(request);
            }
            return response;
        }

        #endregion

        #region private methods
        private Response<UserDto> ValidateUser(Request<UserDto> request)
        {
            response.Status = true;
            return response;
        }
        #endregion
    }
}