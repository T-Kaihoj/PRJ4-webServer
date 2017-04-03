using System.Configuration;
using System.Data.Entity.Migrations;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Common;
using Common.Models;
using DAL;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using MVC.Identity;
using SimpleInjector;
using SimpleInjector.Diagnostics;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;

namespace MVC
{
    [ExcludeFromCodeCoverage]
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Create a container for dependency injection.
            var container = new Container();

            // Set the default life for objects.
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            // Register the components.
            container.Register<IFactory, Factory>(Lifestyle.Singleton);
            container.Register<IStore, Store>(Lifestyle.Transient);
            container.Register<IUserStore<IdentityUser, string>, Store>(Lifestyle.Transient);
            container.Register<IUserContext, UserContext>();
            container.Register<IUtility, Utility>();
            container.Register<IAuthenticationManager, EmptyAuthenticationManager>();

            // Register the MVC controllers.
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            // Suppress transient warnings for IStore and IUserStore.
            var iStoreReg = container.GetRegistration(typeof(IStore)).Registration;
            iStoreReg.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Dispose does nothing in the implementation.");

            var iUserStoreReg = container.GetRegistration(typeof(IUserStore<IdentityUser, string>)).Registration;
            iUserStoreReg.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Dispose does nothing in the implementation.");

            var userManagerReg = container.GetRegistration(typeof(UserManager<IdentityUser, string>)).Registration;
            userManagerReg.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Dispose does nothing in the implementation.");

            // Verification and self-diagnostics.
            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));


            // Update Database to latest migration
            if (bool.Parse(ConfigurationManager.AppSettings["MigrateDatabaseToLatestVersion"]))
            {
                var configuration = new DAL.Migrations.Configuration();
                var migrator = new DbMigrator(configuration);
                migrator.Update();
            }


        }
    }
}
