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

namespace Library.Application.Book.Command.DeleteBook
{
    public class DeleteBookHandler(DataContext context) : ICommandHandler<DeleteBookCommand.Request>
    {
        public async Task<Result> Handle(DeleteBookCommand.Request request, CancellationToken cancellationToken)
        {
            var result = new Result();
            var book = await context.Books.FirstOrDefaultAsync(x => x.Id == request.Id , cancellationToken);
            result.FailIfNullOrEmpty(book, ErrorKey.BookNotExist.ToString(), ResultStatus.NotFound)
                  .FailIf(book.TotalCopies != book.CopiesAvailable, ErrorKey.ThereAreBorrowedCopiesOfThisBook.ToString());
            using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                book.IsDeleted = true;
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
