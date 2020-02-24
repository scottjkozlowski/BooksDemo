using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Odh.BooksDemo.Web.Infrastructure;

namespace Odh.BooksDemo.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            IEnumerable<Func<ControllerContext, ActionDescriptor, object>> conditions =
       new Func<ControllerContext, ActionDescriptor, object>[] {
        // Ensure all POST actions are automatically
        // decorated with the ValidateAntiForgeryTokenAttribute.
( c, a ) => string.Equals( c.HttpContext.Request.HttpMethod, "POST",
        StringComparison.OrdinalIgnoreCase ) ?
        new ValidateAntiForgeryTokenAttribute() : null
    };
            var provider = new ConditionalFilterProvider(conditions);
            // This line adds the filter we created above
            FilterProviders.Providers.Add(provider);
        }
    }
}
