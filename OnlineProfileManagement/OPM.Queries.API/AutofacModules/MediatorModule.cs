using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using FluentValidation;
using MediatR;
using OPM.Queries.API.Queries;
using OPM.Queries.API.QueryHandlers;
using OPM.Queries.API.Behaviors;


namespace OPM.Queries.API.AutofacModules
{
    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterAssemblyTypes(typeof(IMediator).Assembly)
                .AsImplementedInterfaces();

            // Register all the query classes (they implement IRequestHandler) in assembly holding the quert
            builder.RegisterAssemblyTypes(typeof(GetMultipleProfilesQuery).Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            
            builder.RegisterAssemblyTypes(typeof(GetProfileQuery).Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            // Register the QuerytHandler classes (they implement IRequest<>) in assembly  

            // Register the Query's Validators (Validators based on FluentValidation library)
            //builder
            //   .RegisterAssemblyTypes(typeof(CreateEntityProfileCommandValidator).GetTypeInfo().Assembly)
            //   .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
            //   .AsImplementedInterfaces();
            //builder
            //    .RegisterAssemblyTypes(typeof(ComChannelCommandValidator).GetTypeInfo().Assembly)
            //    .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
            //    .AsImplementedInterfaces();


            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            });

            //Register behaviors in behavior pipeline.
            builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
    

        }
    }
}
