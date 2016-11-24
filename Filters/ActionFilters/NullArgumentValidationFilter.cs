using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Web.API.Filters.Filters.ActionFilters
{
    public class NullArgumentValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (actionContext.ActionArguments.ContainsValue(null))
            {
                var nullArguments = string.Join(",",
                    actionContext.ActionArguments.Where(a => a.Value == null).Select(k => k.Key));

                var message = string.Format("Arguments cannot be null: {0}", nullArguments);
                var errorResponse =
                    actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, message);

                actionContext.Response = errorResponse;
            }
        }
    }
}