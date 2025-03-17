using Library.Application.Abstraction;
using Library.Domain.Attribute;

namespace Library.Application.Borrow.Command.BorrowBook
{
    public class BorrowBookCommand
    {
        public sealed record Request([IsRequired] Guid PatronId , [IsRequired] Guid BookId) : ICommand;
    }
}
