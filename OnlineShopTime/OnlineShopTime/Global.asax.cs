using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace OnlineShopTime
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e) {
            SetCulture();
        }

        private void SetCulture() {
            var handler = Context.Handler as MvcHandler;
            var routeData = handler != null ? handler.RequestContext.RouteData : null;
            var routeCulture = routeData != null ? routeData.Values["culture"].ToString() : null;
            if (routeCulture == "default") { routeCulture = null; }
            var languageCookie = HttpContext.Current.Request.Cookies["lang"];
            var userLanguages = HttpContext.Current.Request.UserLanguages;

            // Set the Culture based on a route, a cookie or the browser settings,
            // or default value if something went wrong
            var cultureInfo = new CultureInfo(
                routeCulture ?? (languageCookie != null
                   ? languageCookie.Value
                   : userLanguages != null
                       ? userLanguages[0]
                       : "en")
            );

            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
        }
    }
}
