using Library.Application.Abstraction;
using Library.Domain.Attribute;
using Library.Domain.Enum;

namespace Library.Application.Book.Command.UpdateBook
{
    public class UpdateBookCommand
    {
        public sealed record Request(
        Guid Id,
        [IsRequired] string Title,
        [IsRequired] string Author,
        [GreaterThan(0)] int PublicationYear,
        [IsRequired] string ISBN,
        [IsRequired] string Publisher,
        [IsRequired] Genre Genre,
        [GreaterThan(0)] int TotalCopies) : ICommand;
    }
}
