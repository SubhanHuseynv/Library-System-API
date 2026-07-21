using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAPI.Application.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException(string name) : base($"{name} name is already exists") { }
    }
}
