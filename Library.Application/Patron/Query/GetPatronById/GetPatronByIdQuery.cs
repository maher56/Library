using Library.Application.Abstraction;
using Library.Domain.Attribute;
using Library.Domain.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Patron.Query.GetPatronById
{
    public class GetPatronByIdQuery
    {
        public sealed record Request([IsRequired] Guid Id) : IQuery<Response>;
        public sealed class Response
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public AddressEntity? Address { get; set; }
            public DateTime MembershipDate { get; set; }
        }
    }
}
