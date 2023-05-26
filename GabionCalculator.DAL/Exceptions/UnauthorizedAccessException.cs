using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GabionCalculator.DAL.Exceptions
{
    public class UnauthorizedAccessException : Exception
    {
        public UnauthorizedAccessException()
        {
        }

        public UnauthorizedAccessException(Type type) : base($"{type} is missing")
        {
        }

        public UnauthorizedAccessException(string? message) : base(message)
        {
        }

        public UnauthorizedAccessException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnauthorizedAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
