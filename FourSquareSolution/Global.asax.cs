using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FourSquareSolution.DbContext;

namespace FourSquareSolution
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var context = new SearchQueryContext();
            context.Database.Initialize(true);
        }
    }
}