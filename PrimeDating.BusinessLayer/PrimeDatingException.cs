using System;

namespace PrimeDating.BusinessLayer
{
    public class PrimeDatingException : Exception
    {
        public PrimeDatingException(string errorMessage, Exception exception)
            :base(errorMessage, exception)
        {
        }

        public PrimeDatingException(string errorMessage)
            : base(errorMessage)
        {
        }
    }
}
