using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using MarksonGroceries.Controllers;
using MarksonGroceries.Controllers.API;
using MarksonGroceries.Models.Data;


namespace MarksonGroceries.App_Start
{
    public class ContainerInitializer
    {
        public static IDependencyResolver SetAutofacContainer()
        {
            var builder = new ContainerBuilder();

            var currentAssembly = typeof (HomeController).Assembly;
            var apiAssembly = typeof (CartController).Assembly;

            builder.RegisterControllers(currentAssembly);
            builder.RegisterApiControllers(apiAssembly);

            builder.RegisterType<GroceriesEntities>().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(currentAssembly)
                .Where(t => t.Name.EndsWith("Initializer"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterFilterProvider();

            IContainer container = builder.Build();

            var webApiResolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = webApiResolver;

            return new AutofacDependencyResolver(container);
        }

    }
}