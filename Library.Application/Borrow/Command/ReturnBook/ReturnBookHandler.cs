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

namespace Library.Application.Borrow.Command.ReturnBook
{
    public class ReturnBookHandler(DataContext context) : ICommandHandler<ReturnBookCommand.Request>
    {
        public async Task<Result> Handle(ReturnBookCommand.Request request, CancellationToken cancellationToken)
        {
            var result = new Result();
            var book = await context.Books.FirstOrDefaultAsync(x => x.Id == request.BookId, cancellationToken);
            var borrow = await context.Borrows.FirstOrDefaultAsync(x => x.PatronId == request.PatronId && x.BookId == request.BookId && x.IsReturned == false);
            result.FailIf(!await context.Patrons.AnyAsync(x => x.Id == request.PatronId, cancellationToken), ErrorKey.PatronNotExist.ToString(), ResultStatus.NotFound)
                  .FailIfNullOrEmpty(book, ErrorKey.BookNotExist.ToString(), ResultStatus.NotFound)
                  .FailIfNullOrEmpty(borrow, ErrorKey.TheBookNotBorrowedByThisPatron.ToString(), ResultStatus.NotFound);
            using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                book.CopiesAvailable += 1;
                borrow.IsReturned = true;
                borrow.ReturnDate = DateTime.UtcNow;
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
