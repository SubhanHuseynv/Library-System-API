using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAPI.Application.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(): base("Authentication required. Please log in.") { }

        public UnauthorizedException(string message): base(message) { }
    }
}
