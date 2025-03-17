using Library.Application.Abstraction;
using Library.Domain.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Patron.Query.GetAllPatrons
{
    public class GetAllPatronsQuery
    {
        public sealed record Request() : IQuery<List<Response>>;
        public sealed class Response
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
        }
    }
}
