using OnlineFoodOrderingService.DTO;
using OnlineFoodOrderingService.DTO.User;
using OnlineFoodOrderingService.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFoodOrderingService.SQLRepository
{
    public class SQLUserRepository:IUserRepository
    {
        Response<UserDto> response;
        
        public SQLUserRepository()
        {
            response = new Response<UserDto>();
        }

        public Response<UserDto> SignUp(Request<UserDto> request)
        {
            return response;
        }
    }
}