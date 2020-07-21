using System;
using FluentValidation;
using OPM.Commands.API.Commands;
using OPM.Commands.API.Models;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;


namespace OPM.Commands.API.Validations
{
    public class ComChannelCommandValidator : AbstractValidator<AddComChannelCommand>
    {
        public ComChannelCommandValidator(ILogger<ComChannelCommandValidator> logger)
        {
            RuleFor(command => command.EntityID).NotEmpty();

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
    }

}
