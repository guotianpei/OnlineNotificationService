using System;
using System.Linq;
using System.Reflection;
using Autofac;
using MediatR;

namespace ONP.BackendProcessor.Infrastructure.AutofacModules
{
    public class MediatorModule:Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            // Register all the Command classes (they implement IRequestHandler) in assembly holding the Commands
            //builder.RegisterAssemblyTypes(typeof(CreateEntityProfileCommand).GetTypeInfo().Assembly)
            //    .AsClosedTypesOf(typeof(IRequestHandler<,>));

            // Register the DomainEventHandler classes (they implement INotificationHandler<>) in assembly holding the Domain Events
            //builder.RegisterAssemblyTypes(typeof(ComChannelAddedDomainEventHandler).GetTypeInfo().Assembly)
            //    .AsClosedTypesOf(typeof(INotificationHandler<>));

            // Register the Command's Validators (Validators based on FluentValidation library)
            //builder
            //   .RegisterAssemblyTypes(typeof(CreateEntityProfileCommandValidator).GetTypeInfo().Assembly)
            //   .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
            //   .AsImplementedInterfaces();
            

            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            });

            //Register behaviors in behavior pipeline.
            //builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            //builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            //builder.RegisterGeneric(typeof(TransactionBehavior<,>)).As(typeof(IPipelineBehavior<,>));


        }
    }
}
