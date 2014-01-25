using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Security.Principal;

namespace Movies.Security
{
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.User.Identity.IsAuthenticated)
            {
                var authCookie = httpContext.Request.Cookies[FormsAuthentication.FormsCookieName];

                if (authCookie != null)
                {
                    string[] roles = new string[1];


                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                    roles[0] = ticket.UserData;

                    var identity = new GenericIdentity(ticket.Name);
                    httpContext.User = new GenericPrincipal(identity, roles);
                }
                else return base.AuthorizeCore(httpContext);
            }
               

            return base.AuthorizeCore(httpContext);
            
        }
    }
}