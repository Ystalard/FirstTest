using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstTest.Handler
{
    public class MustRestartException: Exception
    {
        public MustRestartException() : base()
        {
            
        }

        public MustRestartException(string? message) : base(message)
        {
            
        }

        public MustRestartException(string? message, Exception? innerException) : base(message,innerException)
        {

        }
    }

    public class NotImplementedException: Exception
    {}
}