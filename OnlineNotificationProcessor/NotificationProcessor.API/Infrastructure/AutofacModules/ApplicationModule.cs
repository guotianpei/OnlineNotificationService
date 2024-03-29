﻿using Autofac;
using MMS.EventBus.Abstractions;
using NotificationProcessor.API.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ONP.Infrastructure.Responsitories;
using ONP.Infrastructure.Repositories.Interfaces;

namespace NotificationProcessor.API.Infrastructure.AutofacModules
{
    public class ApplicationModule : Autofac.Module
    {
      
        public ApplicationModule(string qconstr)
        {
        

        }

        protected override void Load(ContainerBuilder builder)
        {

          

            builder.RegisterType<NotificationRequestRepository>()
               .As<INotificationRequestRepository>()
               .InstancePerLifetimeScope();

            builder.RegisterType<NotificationTemplateRepository>()
                .As<INotificationTemplateRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<EntityProfileRepository>()
               .As<IEntityProfileRepository>()
               .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(NotificationRequestedIntegrationEventHandler).GetTypeInfo().Assembly)
            .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));

        }
    }
}
