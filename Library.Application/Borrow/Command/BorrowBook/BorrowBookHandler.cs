using Library.Application.Abstraction;
using Library.Domain.Base;
using Library.Domain.Enum;
using Library.Domain.Main;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Borrow.Command.BorrowBook
{
    public class BorrowBookHandler(DataContext context) : ICommandHandler<BorrowBookCommand.Request>
    {
        public async Task<Result> Handle(BorrowBookCommand.Request request, CancellationToken cancellationToken)
        {
            var result = new Result();
            var book = await context.Books.FirstOrDefaultAsync(x => x.Id == request.BookId, cancellationToken);
            result.FailIf(!await context.Patrons.AnyAsync(x => x.Id == request.PatronId, cancellationToken), ErrorKey.PatronNotExist.ToString(), ResultStatus.NotFound)
                  .FailIfNullOrEmpty(book, ErrorKey.BookNotExist.ToString(), ResultStatus.NotFound)
                  .FailIf(await context.Borrows.AnyAsync(x => x.PatronId == request.PatronId && x.BookId == request.BookId && x.IsReturned == false, cancellationToken), ErrorKey.ThebookAlreadyBorrowedFromThisReader.ToString())
                  .FailIf(book.CopiesAvailable == 0, ErrorKey.ThereIsNoRemainingCopies.ToString(), ResultStatus.NotFound);
            using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                book.CopiesAvailable -= 1;
                await context.Borrows.AddAsync(new BorrowEnttity
                {
                    BookId = request.BookId,
                    PatronId = request.PatronId,
                    IsReturned = false,
                    // DueDate
                } , cancellationToken);
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
