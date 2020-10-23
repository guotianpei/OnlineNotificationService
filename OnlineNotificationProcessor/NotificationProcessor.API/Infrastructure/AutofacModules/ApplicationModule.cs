using Autofac;
using MMS.EventBus.Abstractions;
using NotificationProcessor.API.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace NotificationProcessor.API.Infrastructure.AutofacModules
{
    public class ApplicationModule : Autofac.Module
    {
        public string QueriesConnectionString { get; }

        public ApplicationModule(string qconstr)
        {
            QueriesConnectionString = qconstr;

        }

        protected override void Load(ContainerBuilder builder)
        {


            builder.RegisterType<NotificationContext>()
                .As<NotificationContext>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(NotificationRequestedIntegrationEventHandler).GetTypeInfo().Assembly)
            .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));

        }
    }
}
