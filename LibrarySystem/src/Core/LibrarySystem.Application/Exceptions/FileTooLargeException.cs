using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.Exceptions
{
    public class FileTooLargeException : Exception
    {
        public FileTooLargeException(string message) : base(message) { }
    }
}
