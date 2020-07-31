using System;
using FluentValidation;
using OPM.Commands.API.Commands;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Collections.Generic;
using OPM.Commands.API.Models;
using Microsoft.EntityFrameworkCore.Internal;
using OPM.Domain.Aggregates.ProfileAggregate;
using FluentValidation.Validators;

namespace OPM.Commands.API.Validations
{
    public class CreateEntityProfileCommandValidator : AbstractValidator<CreateEntityProfileCommand>
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

            //RuleFor(command => command.ComChannels)
            //    .Cascade(CascadeMode.StopOnFirstFailure)
            //    .Must(ComChannelsAvailable)
            //    .WithMessage("No ComChannels Available");

            RuleFor(command => command.ComChannels)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .Must(ComChannelsAvailable)
                .WithMessage("ComChannels cannot be empty.")
                .Must(InvalidComChannels)
                .WithMessage("ComChannels type or value cannot be empty.")
                .Must(ValidTypeComChannels)
                .WithMessage("Multiple ComChannels with same type are not allowed.")
                .Must(ValidValueComChannels)
                .WithMessage("Multiple ComChannels with same value are not allowed.");

            RuleFor(command => command.ComChannels)
                .NotNull()
                .Must(ValidateEmail)
                .WithMessage("Invalid Email.");

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
        private bool ComChannelsAvailable(IEnumerable<ComChannel> comChannels)
        {
            return (comChannels != null && comChannels.Count() > 0);
        }
        private bool InvalidComChannels(IEnumerable<ComChannel> comChannels)
        {
            return (comChannels.Where(i => i.ComChannelTypes != null && !string.IsNullOrEmpty(i.Value) && !string.IsNullOrWhiteSpace(i.Value))
                               .Select(i => i)
                               .Count() > 0);
        }
        private bool ValidTypeComChannels(IEnumerable<ComChannel> comChannels)
        {
            //TO-DO: for each channel, to valid ComChannelTypes, and its value mobile/email with regularExpress
            if (comChannels != null && comChannels.Count() > 0)
            {
                var groupByTypeCount = comChannels.GroupBy(c => c.ComChannelTypes.Name)
                                       .Select(c => new {
                                           type = c.Key,
                                           typeCount = c.Count()
                                       });
                foreach (var type in groupByTypeCount)
                {
                    return (type.typeCount == 1);
                }
            }
            return true;
        }
        private bool ValidValueComChannels(IEnumerable<ComChannel> comChannels)
        {
            //TO-DO: for each channel, to valid ComChannelTypes, and its value mobile/email with regularExpress
            if (comChannels != null && comChannels.Count() > 0)
            {
                var groupByValueCount = comChannels.GroupBy(c => c.Value)
                                       .Select(c => new {
                                           value = c.Key,
                                           valueCount = c.Count()
                                       });
                foreach (var value in groupByValueCount)
                {
                    return (value.valueCount == 1);
                }
            }
            return true;
        }
        private bool ValidateEmail(IEnumerable<ComChannel> comChannels)
        {
            //TO-DO: for each channel, to valid ComChannelTypes, and its value mobile/email with regularExpress
            if (comChannels != null && comChannels.Count() > 0)
            {
                foreach (ComChannel type in comChannels)
                {
                    //if (type.ComChannelTypes.Id == 1)
                    //    return (string.IsNullOrEmpty(Convert.ToString(RuleFor(t => type.ComChannelTypes.Name).EmailAddress())));
                }
            }
            return true;
        }
    }
}