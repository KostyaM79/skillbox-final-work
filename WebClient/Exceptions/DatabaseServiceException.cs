using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exceptions
{
    public class DatabaseServiceException : Exception
    {
        public DatabaseServiceException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public int StatusCode { get; private set; }
    }
}
