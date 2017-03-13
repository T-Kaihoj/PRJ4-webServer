using System.Web.Optimization;

namespace MVC
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // JavaScript for libraries.
            var bundleJsLibs = new ScriptBundle("~/bundles/javascript-libs").Include(
                "~/Content/assets/js/jquery.js",
                "~/Content/assets/js/jquery.validate.js",
                "~/Content/assets/js/moment.js",
                "~/Content/assets/js/bootstrap.js",
                "~/Content/assets/js/bootstrap-datetimepicker.js",
                "~/Content/assets/js/material.js",
                "~/Content/assets/js/ripples.js"
            );

            // We need to specify the load order, so we overload the ordering.
            bundleJsLibs.Orderer = new PassthruBundleOrderer();

            // Add to the global list of bundles.
            bundles.Add(bundleJsLibs);

            // JavaScript for the site.
            bundles.Add(new ScriptBundle("~/bundles/javascript-site").Include(
                "~/Content/js/site.js"
            ));

            // Stylesheets for the libraries.
            bundles.Add(new StyleBundle("~/bundles/css-libs").Include(
                "~/Content/assets/css/*.css"
            ));
            
            // Stylesheets for the site.
            bundles.Add(new StyleBundle("~/bundles/css-site").Include(
                "~/Content/site.css"
            ));
        }
    }
}
