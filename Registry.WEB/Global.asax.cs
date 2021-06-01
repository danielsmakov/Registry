using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using Registry.BLL.Infrastructure;
using Registry.WEB.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Registry.WEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            

            NinjectModule serviceModule = new ServiceModule();
            NinjectModule organizationModule = new OrganizationModule();
            var kernel = new StandardKernel(serviceModule, organizationModule);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}
