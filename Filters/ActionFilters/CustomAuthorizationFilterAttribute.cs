using System;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Web.API.Filters.Filters.ActionFilters
{
    public class CustomAuthorizationFilterAttribute : AuthorizeAttribute
    {
        public IAuthorisationHelper Helper { get; set; }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var authorizeHeader = actionContext.Request.Headers.Authorization;
            if (authorizeHeader != null
                && authorizeHeader.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase)
                && String.IsNullOrEmpty(authorizeHeader.Parameter) == false)
            {
                var encoding = Encoding.GetEncoding("ISO-8859-1");
                var credintials =
                    encoding.GetString(Convert.FromBase64String(authorizeHeader.Parameter));

                string username = credintials.Split(':')[0];
                string password = credintials.Split(':')[1];
                string roleOfUser;

                //Helper = actionContext.Request.GetDependencyScope().GetService(typeof(IAuthorisationHelper)) as IAuthorisationHelper;

                if (AuthorisationHelper.IsValidUser(username, password, out roleOfUser))
                {
                    var principal =
                        new GenericPrincipal((new GenericIdentity(username)),
                            (new[] { roleOfUser }));

                    Thread.CurrentPrincipal = principal;
                    return;
                }
            }

            actionContext.Response =
                actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            actionContext.Response.Content =
                new StringContent("Username and/or password are missings or invalid");

        }
    }

    public interface IAuthorisationHelper
    {
        bool IsValidUser(string userName, string password, out string roles);
    }

    // Get it from DB or some where???
    public class AuthorisationHelper : IAuthorisationHelper
    {
        public static bool IsValidUser(string userName, string password, out string roles)
        {
            if (userName == "rahman" && password == "rahman")
            {
                roles = "Admin";
                return true;
            }

            roles = "";
            return false;
        }

        bool IAuthorisationHelper.IsValidUser(string userName, string password, out string roles)
        {
            return IsValidUser(userName, password, out roles);
        }
    }
}