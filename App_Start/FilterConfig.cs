using System.Web;
using System.Web.Mvc;
using Web.API.Filters.Filters.ActionFilters;

namespace Web.API.Filters
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            
        }
    }
}
