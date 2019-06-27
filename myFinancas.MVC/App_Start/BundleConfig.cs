using System.Web;
using System.Web.Optimization;

namespace myFinancas.MVC
{
    public class BundleConfig
    {
        // Para obter mais informações sobre o agrupamento, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {     
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use a versão em desenvolvimento do Modernizr para desenvolver e aprender. Em seguida, quando estiver
            // pronto para a produção, utilize a ferramenta de build em https://modernizr.com para escolher somente os testes que precisa.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));
           
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            // CSS Files
            bundles.Add(new StyleBundle("~/Content/assets/css/material-dashboard").Include(
                "~/Content/assets/css/material-dashboard.css"));

            // CSS Just for demo purpose, don't include it in your project
            bundles.Add(new StyleBundle("~/Content/assets/demo-css").Include(
                "~/Content/assets/demo/demo.css"));

            // Core JS Files 
            // ../assets/css/material-dashboard.css?v=2.1.0 versão retirada
            bundles.Add(new ScriptBundle("~/Content/assets/js/core").Include(
                "~/Content/assets/js/core/jquery.min.js",
                "~/Content/assets/js/core/popper.min.js",
                "~/Content/assets/js/core/bootstrap-material-design.min.js",
                "~/Content/assets/js/plugins/perfect-scrollbar.jquery.min.js"));

            // Chartist JS and Notifications Plugin
            bundles.Add(new ScriptBundle("~/Content/js/plugins").Include(
                "~/Content/assets/js/plugins/chartist.min.js",
                "~/Content/assets/js/plugins/bootstrap-notify.js"));

            // Control Center for Material Dashboard: parallax effects, scripts for the example pages etc
            // ../assets/js/material-dashboard.js?v=2.1.0 versão retirada
            bundles.Add(new ScriptBundle("~/Content/js/material-dashboard").Include(
                "~/Content/assets/js/material-dashboard.js"));

            // Material Dashboard DEMO methods, don't include it in your project!
            bundles.Add(new StyleBundle("~/Content/assets/demo-js").Include(
                "~/Content/assets/demo/demo.js"));
        }
    }
}
