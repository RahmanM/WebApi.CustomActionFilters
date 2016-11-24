using System.Diagnostics;
using System.Web.Http.Filters;
using System.Web.UI.WebControls;

namespace Web.API.Filters.Filters.ActionFilters
{
    public class PerformanceLoggerFilter : ActionFilterAttribute
    {

        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);
            actionContext.Request.Properties[actionContext.ActionDescriptor.ActionName] = Stopwatch.StartNew();
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var actionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;
            var fullActionName = actionExecutedContext.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName + "." + actionName;
            var stopWatch = actionExecutedContext.Request.Properties[actionName] as Stopwatch;
            if (stopWatch != null)
                Trace.WriteLine(string.Format("Finished action: {0} -> {1}", fullActionName, stopWatch.Elapsed));
        }

    }
}