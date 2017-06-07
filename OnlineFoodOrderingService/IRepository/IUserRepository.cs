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
        Response<UserDto> GetLogInDetails(Request<UserDto> request);
        Response<UserDto> UpdateProfile(Request<UserDto> request);
        Response<UserDto> ChangeDeliveryAddress(Request<UserDto> request);
    }
}