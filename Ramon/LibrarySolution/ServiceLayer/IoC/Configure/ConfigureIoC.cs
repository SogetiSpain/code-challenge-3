using Castle.Windsor;
using Castle.Windsor.Installer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.IoC.Configure
{
    public static class ConfigureIoC
    {
        public static void Configure(IWindsorContainer container)
        {
            container.Install(FromAssembly.This());
        }
    }
}
