using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using PrimeDating.BusinessLayer;
using Unity;

namespace PrimeDatingSaver
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalConfiguration.Configure(Register);
        }

        private static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();

            Bootstraper.Register(container);

            PrimeDating.Reports.Bootstraper.Register(container);

            GlobalConfiguration.Configuration.DependencyResolver = new UnityResolver(container);

            ControllerBuilder.Current.SetControllerFactory(new IocControllerFactory(container));
        }
    }
}
