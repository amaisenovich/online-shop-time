using System.Web;
using System.Web.Optimization;

namespace OnlineShopTime
{
    public class BundleConfig
    {
        // Дополнительные сведения об объединении см. по адресу: http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-2.1.1.min.js",
                        "~/Scripts/jquery.rateit.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // используйте средство построения на сайте http://modernizr.com, чтобы выбрать только нужные тесты.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/light_css").Include(
                      "~/Content/lumen_bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/rateit.css"));

            bundles.Add(new StyleBundle("~/Content/dark_css").Include(
                      "~/Content/darkly_bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/rateit.css"));
        }
    }
}
