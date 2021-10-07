using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using CodingExercise.DAL;
using CodingExercise.Services;

namespace CodingExercise
{
    public class IoCConfig
    {
        //private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public static void Configure()
        {
            var builder = new ContainerBuilder();

            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            // Register MVC controllers.
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            // Register Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);

            // OPTIONAL: Register the Autofac model binder provider.
            builder.RegisterWebApiModelBinderProvider();

            builder.RegisterType<UserManager>().As<IUserManager>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<PasswordService>().As<IPasswordService>();


            // Set the dependency resolver to be Autofac.
            var container = builder.Build();

            //for mvc 
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            //for web api
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            //Logger.Info("test");
        }
    }
}