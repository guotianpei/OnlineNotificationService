using FluentValidation;
using Microsoft.Extensions.Logging;
using OPM.Queries.API.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPM.Queries.API.Validations
{
    public class GetProfileQueryValidator : AbstractValidator<GetProfileQuery>
    {
        public GetProfileQueryValidator(ILogger<GetProfileQueryValidator> logger)
        {
            RuleFor(command => command.entityID)
                .NotEmpty()
                .Matches("^[a-zA-Z0-9]*$")
                .WithMessage("Special characters are not allowed.");

            RuleFor(command => command.entityID)
                .NotEmpty()
                .Must(MaxLengthEntityID)
                .WithMessage("Length cannot be more than 20 characters.");

            logger.LogTrace("Instance created - {ClassName}", GetType().Name);

        }
        private bool MaxLengthEntityID(string entityID)
        {
            return entityID.Length<=20;
        }
    }
}
