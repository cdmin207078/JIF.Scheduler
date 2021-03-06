﻿using Autofac;
using Autofac.Integration.Mvc;
using Common.Logging;
using Common.Logging.Configuration;
using Common.Logging.NLog;
using JIF.Scheduler.Core.Configuration;
using JIF.Scheduler.Core.Data;
using JIF.Scheduler.Core.Infrastructure;
using JIF.Scheduler.Core.Infrastructure.DependencyManagement;
using JIF.Scheduler.Core.Log;
using JIF.Scheduler.Core.Services.Jobs;
using JIF.Scheduler.Data.EntityFramework;
using Quartz;
using System.Data.Entity;
using System.Web;

namespace JIF.Scheduler.Web
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, JIFConfig config)
        {
            // register HTTP context and other related stuff
            builder.Register(c => new HttpContextWrapper(HttpContext.Current) as HttpContextBase)
                .As<HttpContextBase>()
                .InstancePerLifetimeScope();

            builder.Register(c => c.Resolve<HttpContextBase>().Request)
                .As<HttpRequestBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Response)
                .As<HttpResponseBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Server)
                .As<HttpServerUtilityBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Session)
                .As<HttpSessionStateBase>()
                .InstancePerLifetimeScope();

            // register your MVC controllers. (MvcApplication is the name of
            // the class in Global.asax.)
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            // OPTIONAL: register dbcontext 
            builder.Register<DbContext>(c => new JIFDbContext("name=JIF.Scheduler.DB")).InstancePerLifetimeScope();

            // OPTIONAL: repositores
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();


            // Core Implements Dependency
            builder.RegisterInstance(new NLogLoggerFactoryAdapter(new NameValueCollection()).GetLogger("")).As<ILog>().SingleInstance();

            // Scheduler
            builder.RegisterType<SchedulerContainer>().SingleInstance();

            // Services
            builder.RegisterType<JobInfoServices>().InstancePerLifetimeScope();
        }

        public int Order { get { return 0; } }

    }
}