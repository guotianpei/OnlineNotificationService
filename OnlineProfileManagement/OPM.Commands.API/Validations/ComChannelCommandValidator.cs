using System;
using FluentValidation;
using OPM.Commands.API.Commands;
using OPM.Commands.API.Models;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;


namespace OPM.Commands.API.Validations
{
    public class ComChannelCommandValidator : AbstractValidator<AddOrUpdateComChannelCommand>
    {
 
        public ComChannelCommandValidator(ILogger<ComChannelCommandValidator> logger)
 
        {
            RuleFor(command => command.EntityID).NotEmpty();

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
                .NotEmpty()
                .Must(ValidComChannels)
                .WithMessage("Invalid Communication Preference value/type.");
            logger.LogTrace("Instance created - {ClassName}", GetType().Name);

        }

        private bool ValidComChannels(IEnumerable<ComChannel> comChannels)
        {
            //TO-DO: for each channel, to valid ComChannelTypes, and its value mobile/email with regularExpress
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
    }
}
