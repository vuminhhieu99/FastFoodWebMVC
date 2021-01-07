using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CnWeb_FastFood.Areas.Admin.Models
{
    public class HasCredentialAttribute : AuthorizeAttribute
    {
        public string id_role { get; set; }
        public string id_userGroud { get; set; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //var isAuthorized = base.AuthorizeCore(httpContext);
            //if (isAuthorized)
            //{
            //    return false;
            //}
            var session = (string)HttpContext.Current.Session[AdminCommonConstants.USER_SESSION];
            if (session == null)
                return false;
            List<string> privilegeLevels = this.GetCredentialByLoggedInUser();
            if (privilegeLevels.Contains(this.id_role))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new ViewResult
            {
                ViewName = "~/Areas/Admin/Views/Shared/error_401.cshtml"
            }  ; 
        }

        private List<string> GetCredentialByLoggedInUser()
        {
            var credentials = (List<string>)HttpContext.Current.Session[AdminCommonConstants.SESSION_CREDENTIAL];
            return credentials;
        }
    }
}