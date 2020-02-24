using System.Web;
using System.Web.Optimization;

namespace Odh.BooksDemo.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/busyIndicator.js",
                        "~/Scripts/customDialog.js",
                        "~/Scripts/dirtyCheck.js",
                        "~/Scripts/topMessageBar.js",
                        "~/Scripts/Encoder.js"
                       ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                  "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/OdhWeb.css"));

            bundles.Add(new StyleBundle("~/Content/kendo/css").Include(
          "~/Content/kendo/2016.2.607/kendo.common-bootstrap.min.css",
          "~/Content/kendo/2016.2.607/kendo.blueopal.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
            "~/Scripts/kendo/2016.2.607/kendo.all.min.js",
            // "~/Scripts/kendo/kendo.timezones.min.js", // uncomment if using the Scheduler
            "~/Scripts/kendo/2016.2.607/kendo.aspnetmvc.min.js"));

            bundles.Add(new StyleBundle("~/Content/Kendo/2016.2.607/css").Include(
              "~/Content/kendo/2016.2.607/kendo.common.min.css",
              //"~/Content/kendo/2016.2.607/kendo.common-bootstrap.min.css",
              "~/Content/kendo/2016.2.607/kendo.blueopal.min.css",
              "~/Content/kendo/2016.2.607/kendo.dataviz.blueopal.min.css"
              ));



            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                "~/Scripts/kendo/2016.2.607/jquery.min.js",
                 "~/Scripts/kendo/2016.2.607/jszip.min.js",
            "~/Scripts/kendo/2016.2.607/kendo.web.min.js",
            // "~/Scripts/kendo/kendo.timezones.min.js", // uncomment if using the Scheduler
            "~/Scripts/kendo/2016.2.607/kendo.aspnetmvc.min.js"


            ));

            //tell asp.net to allow minified files in debug mode
            bundles.IgnoreList.Clear();
            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }


}
