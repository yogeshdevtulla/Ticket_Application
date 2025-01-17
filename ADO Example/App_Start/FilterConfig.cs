using System.Web;
using System.Web.Mvc;
using ADO_Example.Filters;
//using ADO_Example.App_Start.Filters;

namespace ADO_Example
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CustomAuthorizrAttribute());
            //filters.Add(new AuthenticationFilter());
            //filters.Add(new SessionTimeoutFilter());
            filters.Add(new GlobalExceptionFilter());
        }
    }
}
