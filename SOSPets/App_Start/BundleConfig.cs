using System.Web;
using System.Web.Optimization;
using Lance.WebSite.New.Helpers.Provider;

namespace SOSPets
{
    public class BundleConfig
    {
        // Para obter mais informações sobre o agrupamento, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //var layout = new ScriptBundle("~/bundles/site").Include(
            //                    "~/Scripts/jquery-{version}.js",
            //                    "~/Scripts/jquery.validate.js",
            //                    "~/JS/Shared/ValidateMessage.js",
            //                    //"~/JS/Plugin/OwlCarousel2/dist/owl.carousel.js",
            //                    "~/Content/lib/slick-carousel/slick.min.js",
            //                    "~/Scripts/bootstrap.js",
            //                    "~/Scripts/bootbox.js",
            //                    "~/JS/Shared/Common.js",
            //                    "~/JS/Shared/Newsletter.js");


            //layout.Orderer = new NonOrderingBundleOrderer();
            //bundles.Add(layout);

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
        }
    }
}
