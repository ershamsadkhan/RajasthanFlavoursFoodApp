using OnlineFoodOrderingService.DTO;
using OnlineFoodOrderingService.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineFoodOrderingService.IRepository
{
    public interface IUserRepository
    {
         Response<UserDto> SignUp(Request<UserDto> request);
        Response<UserDto> GetUserDetails(Request<UserDto> request);
    }
}
