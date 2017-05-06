using OnlineFoodOrderingService.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Ninject;
using OnlineFoodOrderingService.Manager;
using OnlineFoodOrderingService.DTO;
using OnlineFoodOrderingService.DTO.User;
using System.Text;
using System.Reflection;
using Ninject.Web.Common;
using OnlineFoodOrderingService.SQLRepository;

namespace OnlineFoodOrderingService.Authorization
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AuthorizeUser : AuthorizeAttribute
    {
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (AuthorizeRequest(actionContext))
            {

                return;
            }
            return;
            HandleUnauthorizedRequest(actionContext);
        }

        protected override void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "You better back Off!" });
        }

        private bool AuthorizeRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            Response<UserDto> response = new Response<UserDto>();
            Request<UserDto> request = new Request<UserDto>();

            var userMnager = new UserManager(new SQLUserRepository());
            var re = actionContext.Request;
            var headers = re.Headers;
            string username = "";
            string password = "";

            if (headers.Contains("username") && headers.Contains("password"))
            {
                username = headers.GetValues("username").First();
                password = headers.GetValues("password").First();
            }


            request.Obj = new UserDto()
            {
                UserName = username,
                UserPwd = password
            };

            response = userMnager.LogIn(request);
            return response.Status;
        }


        private bool IsOwnerOfPost(string username, string postId)
        {
            // TODO: you know what to do here
            throw new NotImplementedException();
        }
    }
}