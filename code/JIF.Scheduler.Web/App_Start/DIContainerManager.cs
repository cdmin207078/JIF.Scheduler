using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIF.Scheduler.Web
{
    class DIContainerManager
    {
        private DIContainerManager() { }

        private IContainer _container;

        internal static void Initialize()
        {

        }

        public IContainer Container
        {
            get
            {
                return _container;
            }
        }


    }
}