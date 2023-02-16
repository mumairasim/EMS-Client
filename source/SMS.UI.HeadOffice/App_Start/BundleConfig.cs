using System.Web.Optimization;

namespace SMS.UI.HeadOffice
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/JsResources").Include(
                       "~/Scripts/jquery-{version}.js",
                       "~/Scripts/modernizr-*",
                       //Angular Scripts
                       "~/Scripts/angular.min.js",
                       "~/Scripts/angular-route.min.js",
                       "~/Scripts/angular-cookies.min.js",
                       "~/Scripts/angular-resource.min.js",
                       "~/Scripts/angular-animate.min.js",
                       //BootStrap
                       "~/Scripts/bootstrap.min.js",
                       //App Scripts
                       "~/Scripts/App/main.js",
                       "~/Scripts/App/checklist-model.js",
                       "~/Scripts/App/angular-growl-notifications.js",
                       "~/Scripts/App/angular-ui-router.min.js",
                       "~/Scripts/App/ng-file-upload-all.js",
                       "~/Scripts/App/ng-pattern-restrict.min.js",
                       "~/Scripts/angular-local-storage.min.js",
                       //External Scripts
                       "~/Scripts/App/js/main.js",
                       //Angular App Scripts
                       "~/App/App.js",
                       "~/App/Services/*.js",
                        //"~/App/Directives/*.js",
                        "~/App/Controllers/*.js",
                        "~/App/Controllers/Authentication/*.js",
                       "~/App/Controllers/Dashboard/*.js",
                        "~/App/Controllers/Modules/School/*.js",
                        "~/App/Controllers/Modules/StudentDiary/*.js",
                    "~/App/Controllers/Modules/Student/*.js",
                       "~/App/Controllers/Modules/Employee/*.js",
                       "~/App/Controllers/Modules/Class/*.js",
                       "~/App/Controllers/UserProfile/*.js",
                       "~/App/Controllers/UserProfile/*.js",
                       "~/App/Controllers/Modules/Worksheet/*.js",
                       "~/App/Controllers/Modules/Course/*.js",
                       "~/App/Controllers/Modules/Designation/*.js",
                       "~/App/Controllers/Modules/LessonPlan/*.js",
                       "~/App/Controllers/Modules/Attendance/*.js",
                       "~/App/Controllers/Modules/FinanceType/*.js",
                       "~/App/Controllers/Modules/StudentFinance/*.js",
                       "~/App/Controllers/Modules/EmployeeFinance/*.js",
                        "~/App/Controllers/Modules/TeacherDiary/*.js",
                       "~/App/Controllers/Modules/Attendance/*.js",
                       "~/App/Controllers/Modules/TimeTable/*.js",
                       "~/App/Controllers/Modules/Requests/*.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      //External Contents
                      "~/Content/bootstrap.css",
                      "~/fonts/font-awesome-4.7.0/css/font-awesome.min.css",
                      "~/fonts/Linearicons-Free-v1.0.0/icon-font.min.css",
                      //"~/Content/App/mainDashboard.css",
                      "~/Content/App/main.css",
                      "~/Content/App/mainDashboard.css",
                      "~/Content/App/util.css",
                      "~/Content/App/OverridedStyle.css",
                      "~/Content/App/customStyle.css"));
        }
    }
}
