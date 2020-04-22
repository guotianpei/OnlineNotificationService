using System;
using System.Collections.Generic;
using System.Text;

namespace OPM.Domain.Exceptions
{
    public class ProfileDomainException :Exception
    {
        public ProfileDomainException()
        {
        }

        public ProfileDomainException(string message):base(message)
        {
        }

        public ProfileDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
