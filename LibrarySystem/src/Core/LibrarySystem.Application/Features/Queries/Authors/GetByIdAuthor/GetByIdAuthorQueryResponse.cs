using LibrarySystem.Application.Dtos.Authors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.Features.Queries.Authors.GetByIdAuthor
{
    public class GetByIdAuthorQueryResponse
    {
        public GetByIdAuthorDto GetAuthor { get; set; }
    }
}
