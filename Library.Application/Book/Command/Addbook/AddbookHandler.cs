using Library.Application.Abstraction;
using Library.Domain.Base;
using Library.Domain.Enum;
using Library.Domain.Main;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Book.Command.Addbook
{
    public class AddbookHandler(DataContext context) : ICommandHandler<AddbookCommand.Request>
    {
        public async Task<Result> Handle(AddbookCommand.Request request, CancellationToken cancellationToken)
        {
            var result = new Result();
            result.FailIf(await context.Books.AnyAsync(x => x.Title.Equals(request.Title), cancellationToken), ErrorKey.TitleAlreadyTaken.ToString())
                  .FailIf(request.ISBN.Length != 13, ErrorKey.ISBNLengthMustBe13.ToString())
                  .FailIf(await context.Books.AnyAsync(x => x.ISBN.Equals(request.ISBN), cancellationToken), ErrorKey.ISBNAlreadyTaken.ToString());
            using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                context.Books.Add(new BookEntity
                {
                    Id = Guid.NewGuid(),
                    Title = request.Title,
                    Author = request.Author,
                    PublicationYear = request.PublicationYear,
                    ISBN = request.ISBN,
                    Publisher = request.Publisher,
                    Genre = request.Genre,
                    CopiesAvailable = request.TotalCopies,
                    TotalCopies = request.TotalCopies
                });
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
