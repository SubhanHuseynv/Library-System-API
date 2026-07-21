using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAPI.Application.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException():base("Access denied.") { }

        public ForbiddenException(string message):base(message) { }
    }
}
