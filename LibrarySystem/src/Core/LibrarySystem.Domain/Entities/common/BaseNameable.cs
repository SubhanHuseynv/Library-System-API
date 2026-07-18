using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Domain.Entities.common
{
    public abstract class BaseNameable : BaseAccountable
    {
        public string Name { get; set; }
    }
}
