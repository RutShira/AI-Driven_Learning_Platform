using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Exception
{
   
        public class ServiceException : System.Exception
    {
            public ServiceException(string message, System.Exception innerException) : base(message, innerException) { }
        }

        public class ValidationException : System.Exception
        {
            public ValidationException(string message) : base(message) { }
        }
        // Add the NotFoundException class definition
        public class NotFoundException : System.Exception
        {
            public NotFoundException(string message) : base(message) { }
        }

    }


