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

namespace Library.Application.Book.Query.GetBookById
{
    public class GetBookByIdHandler(DataContext context) : IQueryHandler<GetBookByIdQuery.Request, GetBookByIdQuery.Response>
    {
        public async Task<Result<GetBookByIdQuery.Response>> Handle(GetBookByIdQuery.Request request, CancellationToken cancellationToken)
        {
            var result = new Result<GetBookByIdQuery.Response>();
            result.FailIf(!await context.Books.AnyAsync(x => x.Id == request.Id , cancellationToken), ErrorKey.BookNotExist.ToString(), ResultStatus.NotFound);
            result.Data = await context.Books
                .AsNoTracking()
                .Select(x => new GetBookByIdQuery.Response
                {
                    Id = x.Id,
                    Title = x.Title,
                    Author = x.Author,
                    PublicationYear = x.PublicationYear,
                    ISBN = x.ISBN,
                    Publisher = x.Publisher,
                    Genre = x.Genre,
                    CopiesAvailable = x.CopiesAvailable,
                    TotalCopies = x.TotalCopies,
                }).FirstAsync(x => x.Id == request.Id , cancellationToken);
                    return result;
        }
    }
}
