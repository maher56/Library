using Library.Application.Abstraction;
using Library.Domain.Attribute;
using Library.Domain.Enum;
using Library.Domain.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Book.Command.Addbook
{
    public class AddbookCommand
    {
        public sealed record Request(
                [IsRequired] string Title,
                [IsRequired] string Author,
                [GreaterThan(0)] int PublicationYear,
                [IsRequired] string ISBN,
                [IsRequired] string Publisher,
                [IsRequired] Genre Genre,
                [GreaterThan(0)] int TotalCopies) : ICommand;
    }
}
