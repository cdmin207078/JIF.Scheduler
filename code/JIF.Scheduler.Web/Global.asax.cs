using JIF.Scheduler.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace JIF.Scheduler.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // initialize engine context
            EngineContext.Initialize(false);

            // initialize Scheduler
            EngineContext.Current.Resolve<SchedulerContext>();

        }
    }
}
