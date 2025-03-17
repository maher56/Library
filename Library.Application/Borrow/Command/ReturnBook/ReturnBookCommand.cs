using Library.Application.Abstraction;
using Library.Domain.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Borrow.Command.ReturnBook
{
    public class ReturnBookCommand
    {
        public sealed record Request([IsRequired] Guid PatronId, [IsRequired] Guid BookId) : ICommand;
    }
}
