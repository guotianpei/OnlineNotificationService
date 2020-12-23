using System;
using System.Collections.Generic;
using System.Text;

namespace ONP.Domain
{
    public class ONPDomainException :Exception
    {
        public ONPDomainException()
        { }

        public ONPDomainException(string message):base(message)
        { }

        public ONPDomainException(string message, Exception innerException)
            :base(message, innerException)
        { }

    }
}
