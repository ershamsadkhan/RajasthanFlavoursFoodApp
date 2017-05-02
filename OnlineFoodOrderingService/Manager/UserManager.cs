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

        #endregion

        #region 
        public string LogIn(Request<UserDto> request)
        {
            response = repository.GetLogInDetails(request);
            if (response.Status == true)
            {
                string UserName = response.ObjList[0].UserName.ToString();
                HttpContext.Current.Session["UserName"] = UserName;
            }
            else
            {
                HttpContext.Current.Session["UserName"] = response.ErrMsg;
            }
            return HttpContext.Current.Session["UserName"].ToString();
        }

        #endregion
        // private methods
     
        #region 
        private Response<UserDto> ValidateUser(Request<UserDto> request)
        {
            response = repository.GetUserDetails(request);
            if (response.Status == true)
            {
                response.Status = false;
                response.ErrMsg = "User already exists";
                
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