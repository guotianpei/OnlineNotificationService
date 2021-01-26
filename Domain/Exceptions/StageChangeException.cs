using System;
using System.Collections.Generic;
using System.Text;

namespace ONP.Domain
{
    public class StageChangeException : Exception
    {
        public StageChangeException()
        { }

        public StageChangeException(string message):base(message)
        { }

        public StageChangeException(string message, Exception innerException)
            :base(message, innerException)
        { }

    }
}
