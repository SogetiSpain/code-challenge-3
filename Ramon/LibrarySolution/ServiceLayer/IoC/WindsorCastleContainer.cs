using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.IoC
{
    public static class WindsorCastleContainer
    {

        private static IWindsorContainer container = null;
        private static readonly object padlock = new object();

        public static IWindsorContainer Instance
        {
            get
            {
                lock (padlock)
                {
                    if (container == null)
                        container = new WindsorContainer();

                    return container;
                }
            }
        }
    }
}
