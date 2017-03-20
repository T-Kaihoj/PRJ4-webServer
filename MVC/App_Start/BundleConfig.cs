using System.Web.Optimization;

namespace MVC
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Javascript for jQuery.
            bundles.Add(new ScriptBundle("~/bundles/js-essential").Include(
                "~/Content/assets/js/jquery.js"
            ));

            // JavaScript for libraries.
            var bundleJsLibs = new ScriptBundle("~/bundles/js-libs").Include(
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
            bundles.Add(new ScriptBundle("~/bundles/js-site").Include(
                "~/Content/js/site.js"
            ));

            // Stylesheets for the libraries.
            var bundleCssLibs = new StyleBundle("~/bundles/css-libs").Include(
                "~/Content/assets/css/bootstrap.css",
                "~/Content/assets/css/bootstrap-theme.css",
                "~/Content/assets/css/bootstrap-material-design.css",
                "~/Content/assets/css/bootstrap-datetimepicker.css",
                "~/Content/assets/css/font-awesome.css",
                "~/Content/assets/css/ripples.css"
            );

            // We need to specify the load order, so we overload the ordering.
            bundleCssLibs.Orderer = new PassthruBundleOrderer();

            // Add to the global list of bundles.
            bundles.Add(bundleCssLibs);

            // Stylesheets for the site.
            bundles.Add(new StyleBundle("~/bundles/css-site").Include(
                "~/Content/css/site.css"
            ));
        }
    }
}
