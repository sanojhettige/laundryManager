using System.Web;
using System.Web.Optimization;

namespace LaundryManagerWeb
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                           "~/Assets/Vendor/jquery/jquery.min.js",
                           "~/Assets/Vendor/bootstrap/js/bootstrap.bundle.min.js",
                            "~/Assets/Vendor/jquery-easing/jquery.easing.min.js",
                            "~/Assets/Scripts/bootbox.js",
                            "~/Assets/Scripts/respond.js",
                            "~/Assets/Vendor/datatables/jquery.dataTables.min.js",
                            "~/Assets/Vendor/datatables/dataTables.bootstrap4.min.js",
                           "~/Assets/scripts/typeahead.bundle.js",
                           "~/Assets/scripts/toastr.js",
                             "~/Assets/scripts/bloodhound.js",
                             "~/Assets/scripts/moment.js"
                             ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Assets/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/datepicker-js").Include(
                        "~/Assets/Vendor/date-picker/bootstrap-datepicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/report-js").Include(
                "~/Assets/Vendor/datatables/dataTables.buttons.min.js",
                "~/Assets/Vendor/datatables/buttons.flash.min.js",
                "~/Assets/Vendor/datatables/jszip.min.js",
                "~/Assets/Vendor/datatables/pdfmake.min.js",
                "~/Assets/Vendor/datatables/vfs_fonts.js",
                "~/Assets/Vendor/datatables/buttons.html5.min.js",
                "~/Assets/Vendor/datatables/buttons.print.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/frontend-js").Include(
                "~/Assets/FE_Assets/js/vendor/modernizr-3.5.0.min.js",
                "~/Assets/FE_Assets/js/vendor/jquery-1.12.4.min.js",
                "~/Assets/FE_Assets/js/popper.min.js",
                "~/Assets/FE_Assets/js/bootstrap.min.js",
                "~/Assets/FE_Assets/js/owl.carousel.min.js",
                "~/Assets/FE_Assets/js/slick.min.js",
                "~/Assets/FE_Assets/js/plugins.js",
                "~/Assets/FE_Assets/js/main.js",
                "~/Assets/scripts/moment.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/datatable-js").Include(
                "~/Assets/Vendor/datatables/jquery.dataTables.min.js",
                            "~/Assets/Vendor/datatables/dataTables.bootstrap4.min.js"
                            ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Assets/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                        "~/Assets/Scripts/app.js"));

            bundles.Add(new ScriptBundle("~/bundles/cartjs").Include(
                "~/Assets/FE_Assets/js/cart.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Assets/Vendor/fontawesome-free/css/all.min.css",
                        "~/Assets/Vendor/datatables/dataTables.bootstrap4.min.css",
                        "~/Assets/Content/typeahead.css",
                        "~/Assets/Content/toastr.css",
                        "~/Assets/Content/app.css"
                      ));
            bundles.Add(new StyleBundle("~/bundles/datepicker-css").Include(
                        "~/Assets/Vendor/date-picker/bootstrap-datepicker.css"));
            bundles.Add(new StyleBundle("~/bundles/report-css").Include(
                "~/Assets/Vendor/datatables/buttons.dataTables.min.css"
                ));

            bundles.Add(new StyleBundle("~/bundles/frontend-css").Include(
                "~/Assets/FE_Assets/css/bootstrap.min.css",
                "~/Assets/FE_Assets/css/owl.carousel.min.css",
                "~/Assets/FE_Assets/css/slicknav.css",
                "~/Assets/FE_Assets/css/flaticon.css",
                "~/Assets/FE_Assets/css/progressbar_barfiller.css",
                "~/Assets/FE_Assets/css/animate.min.css",
                "~/Assets/FE_Assets/css/slick.css",
                "~/Assets/FE_Assets/css/nice-select.css",
                "~/Assets/FE_Assets/css/style.css",
                "~/Assets/FE_Assets/css/custom.css"
                ));

            bundles.Add(new StyleBundle("~/bundles/datatable-css").Include(
                       "~/Assets/Vendor/datatables/dataTables.bootstrap4.min.css"
                     ));


        }
    }
}
