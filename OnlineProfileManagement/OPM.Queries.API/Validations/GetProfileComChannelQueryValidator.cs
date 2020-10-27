using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Logging;
using OPM.Queries.API.Queries;


namespace OPM.Queries.API.Validations
{
    public class GetProfileComChannelQueryValidator : AbstractValidator<GetProfileComChannelQuery>
    {
        public GetProfileComChannelQueryValidator(ILogger<GetProfileComChannelQueryValidator> logger)
        {
            RuleFor(command => command.EntityIDs)
                .NotEmpty()
                .Matches("^[a-zA-Z0-9,]*$")
                .WithMessage("Special characters are not allowed for Entity IDs.");

            RuleFor(command => command.EntityIDs)
                .NotEmpty()
                .Must(IsEntityIDBlank)
                .WithMessage("Entity IDs is required.");

            logger.LogTrace("Instance created - {ClassName}", GetType().Name);

        }
        private bool IsEntityIDBlank(string entityID)
        {
            return entityID.Length > 0;
        }
    }

}
