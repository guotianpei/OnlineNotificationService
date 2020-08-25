using System;
using System.Reflection;
using Autofac;
using MMS.EventBus.Abstractions;
using OPM.Commands.API.CommandHandlers;
using OPM.Infrastructure.Idempotency;
using OPM.Infrastructure.Repositories;
using OPM.Infrastructure.Repositories.Interfaces;

namespace OPM.Commands.API.Infrastructure.AutofacModules
{

    //Introduction to Dependency Injection in ASP.NET Core
    //https://docs.microsoft.com/aspnet/core/fundamentals/depende
    //ncy-injection
    //Autofac.Official documentation.
    //https://docs.autofac.org/en/latest/

    public class ApplicationModule : Autofac.Module
    {
        public string QueriesConnectionString { get; }

        public ApplicationModule(string qconstr)
        {
            QueriesConnectionString = qconstr;

        }

        protected override void Load(ContainerBuilder builder)
        {


            builder.RegisterType<ProfileRepository>()
                .As<IProfileRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<GroupRepository>()
                .As<IGroupRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<RequestManager>()
               .As<IRequestManager>()
               .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(CreateEntityProfileCommandHandler).GetTypeInfo().Assembly)
            .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));

        }
    }
}
