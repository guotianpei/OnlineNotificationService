﻿using System;
using System.Reflection;
using Autofac;
using OPM.Infrastructure.Idempotency;
using OPM.Infrastructure.Repositories;
using OPM.Infrastructure.Repositories.Interfaces;

namespace OPM.Commands.API.Infrastructure.AutofacModules
{

//Introduction to Dependency Injection in ASP.NET Core

    public class ApplicationModule : Autofac.Module
    {
        public ApplicationModule() 
        {
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

        

    }
}
}