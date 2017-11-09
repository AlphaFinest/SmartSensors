using System.Web;
using System.Web.Optimization;

namespace SmartSensors
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/newJquery").Include(
                        "~/Scripts/newJquery.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/jsgridd").Include(
                     "~/Scripts/jsgrid.js",
                     "~/Scripts/jsgrid.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));              

             bundles.Add(new StyleBundle("~/Content/jsgrid").Include(
                      "~/Content/jsgrid-theme.css",
                       "~/Content/jsgrid.min.css",
                        "~/Content/jsgrid.css",
                      "~/Content/jsgrid-theme.min.css"));

        }
    }
}
