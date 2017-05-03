using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

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

            HandleUnauthorizedRequest(actionContext);

        }

        protected override void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext)

        {

            //Code to handle unauthorized request

        }

        private bool AuthorizeRequest(System.Web.Http.Controllers.HttpActionContext actionContext)

        {

            //Write your code here to perform authorization

            return true;

        }


        private bool IsOwnerOfPost(string username, string postId)
        {
            // TODO: you know what to do here
            throw new NotImplementedException();
        }
    }
}