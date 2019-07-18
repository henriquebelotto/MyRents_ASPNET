using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using AutoMapper;
using MyRents.App_Start;
using MyRents.Models;

namespace MyRents
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Configuring the Automapper
            Mapper.Initialize(cfg => cfg.AddProfile<MappingProfile>());

            // Standard MVC setup
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Using Autofac.mvc 5 to configure Dependency Injection
            // https://autofac.readthedocs.io/en/latest/integration/mvc.html#register-controllers
            var builder = new ContainerBuilder();

            // used for WebApi
            var config = GlobalConfiguration.Configuration;


            // Register your MVC controllers. (MvcApplication is the name of
            // the class in Global.asax.)
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register the Autofac model binder provider. - WEB API
            builder.RegisterWebApiModelBinderProvider();

            // OPTIONAL: Register the Autofac filter provider. - WEB API
            builder.RegisterWebApiFilterProvider(config);

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            builder.RegisterType<ApplicationDbContext>().AsSelf().InstancePerRequest();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            
            // used for WebApi
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
