using System;
using FluentValidation;
using OPM.Commands.API.Commands;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace OPM.Commands.API.Validations
{
    public class CreateEntityProfileCommandValidator :AbstractValidator<CreateEntityProfileCommand>
    {
        public CreateEntityProfileCommandValidator(ILogger<CreateEntityProfileCommandValidator> logger)
        {
            RuleFor(command => command.EntityId).NotEmpty();
            RuleFor(command => command.EntityName).NotEmpty();
            RuleFor(command => command.EntityType).NotEmpty();
            RuleFor(command => command.FirstName).NotEmpty();
            RuleFor(command => command.LastName).NotEmpty();
            RuleFor(command => command.EntityType)
                .NotEmpty()
                .Must(ValidEntityType)
                .WithMessage("Invalid Entity Type");
            RuleFor(command => command.ResourceName)
                .NotEmpty()
                .Must(ValidResourceName)
                .WithMessage("Incorrect Resource Name");
            logger.LogTrace("Instance created - {ClassName}", GetType().Name);

        }

        private bool ValidEntityType(string entityType)
        {
            //TO-DO: Entity Type list should from cache
            return true;
        }

        private bool ValidResourceName(string resourceName)
        {
            //TO-DO: Resource Name list should from cache
            return true;
        }
    }
}
