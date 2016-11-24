using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Web.API.Filters.Filters.ActionFilters
{
    public class ModelStateValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                var errorResponse = 
                    actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest,actionContext.ModelState);

                actionContext.Response = errorResponse;
            }
        }
    }
}