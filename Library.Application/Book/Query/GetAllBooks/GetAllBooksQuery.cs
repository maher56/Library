using Library.Application.Abstraction;
using Library.Domain.Enum;
using Library.Domain.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Book.Query.GetAllBooks
{
    public class GetAllBooksQuery
    {
        public sealed record Request() : IQuery<List<Response>>;
        public sealed class Response
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public Genre Genre { get; set; }
            public int CopiesAvailable { get; set; }
        }
    }
}
