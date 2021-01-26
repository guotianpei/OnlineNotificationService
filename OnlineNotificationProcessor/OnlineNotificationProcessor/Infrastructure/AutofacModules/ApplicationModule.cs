using System;
using System.Reflection;
using Autofac;
using MMS.EventBus.Abstractions;
using ONP.Infrastructure.Responsitories;
using ONP.Infrastructure.Repositories.Interfaces;

namespace ONP.BackendProcessor.Infrastructure.AutofacModules
{

    //Introduction to Dependency Injection in ASP.NET Core
    //https://docs.microsoft.com/aspnet/core/fundamentals/depende
    //ncy-injection
    //Autofac.Official documentation.
    //https://docs.autofac.org/en/latest/

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

            

        }
    }
}
