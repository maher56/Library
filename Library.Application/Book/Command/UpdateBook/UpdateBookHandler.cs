using Library.Application.Abstraction;
using Library.Domain.Base;
using Library.Domain.Enum;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Book.Command.UpdateBook
{
    public class UpdateBookHandler(DataContext context) : ICommandHandler<UpdateBookCommand.Request>
    {
        public async Task<Result> Handle(UpdateBookCommand.Request request, CancellationToken cancellationToken)
        {
            var result = new Result();
            var book = await context.Books.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            result.FailIfNullOrEmpty(book, ErrorKey.BookNotExist.ToString(), ResultStatus.NotFound)
                  .FailIf(await context.Books.AnyAsync(x => x.Id != request.Id && x.Title.Equals(request.Title), cancellationToken), ErrorKey.TitleAlreadyTaken.ToString())
                  .FailIf(request.ISBN.Length != 13, ErrorKey.ISBNLengthMustBe13.ToString())
                  .FailIf(await context.Books.AnyAsync(x => x.Id != request.Id && x.ISBN.Equals(request.ISBN), cancellationToken), ErrorKey.ISBNAlreadyTaken.ToString())
                  .FailIf(book.TotalCopies - book.CopiesAvailable > request.TotalCopies, ErrorKey.TotalCopiesIsLessThanBorrowedCopies.ToString());
            using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                book.Title = request.Title;
                book.Author = request.Author;
                book.PublicationYear = request.PublicationYear;
                book.ISBN = request.ISBN;
                book.Publisher = request.Publisher;
                book.Genre = request.Genre;
                book.CopiesAvailable = request.TotalCopies - (book.TotalCopies - book.CopiesAvailable);
                book.TotalCopies = request.TotalCopies;
                book.UpdatedAt = DateTime.UtcNow;
                await context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return result;
            }catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}