using Autofac;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneioLuta.Controllers;
using TorneioLuta.Services;

namespace TorneioLuta.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void ConfigureContainer(ContainerBuilder builder, IConfiguration configuration)
        {
            builder.RegisterType<TorneioService>()
                .As<ITorneioService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<LutaService>()
                .As<ILutaService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<FasesService>()
                .As<IFasesService>()
                .InstancePerLifetimeScope();
        }
    }
}
