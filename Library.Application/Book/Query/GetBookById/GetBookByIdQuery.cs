using Library.Application.Abstraction;
using Library.Domain.Attribute;
using Library.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Book.Query.GetBookById
{
    public class GetBookByIdQuery
    {
        public sealed record Request([IsRequired] Guid Id) : IQuery<Response>;
        public sealed class Response
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public int PublicationYear { get; set; }
            public string ISBN { get; set; }
            public string Publisher { get; set; }
            public Genre Genre { get; set; }
            public int CopiesAvailable { get; set; }
            public int TotalCopies { get; set; }
        }
    }
}
