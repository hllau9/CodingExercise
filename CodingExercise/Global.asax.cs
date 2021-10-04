using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;

namespace CodingExercise
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            IoCConfig.Configure(); //IoC container 
            GlobalConfiguration.Configure(WebApiConfig.Register); //web api routing
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
