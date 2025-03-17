using Library.Application.Abstraction;
using Library.Domain.Base;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Book.Query.GetAllBooks
{
    public class GetAllBooksHandler(DataContext context) : IQueryHandler<GetAllBooksQuery.Request, List<GetAllBooksQuery.Response>>
    {
        public async Task<Result<List<GetAllBooksQuery.Response>>> Handle(GetAllBooksQuery.Request request, CancellationToken cancellationToken)
        {
            var result = new Result<List<GetAllBooksQuery.Response>>();
            result.Data = await context.Books
                .AsNoTracking()
                .OrderBy(x => x.CreatedAt)
                .Select(x => new GetAllBooksQuery.Response
                {
                    Id = x.Id,
                    Title = x.Title,
                    Author = x.Author,
                    Genre = x.Genre,
                    CopiesAvailable = x.CopiesAvailable,
                }).ToListAsync(cancellationToken);
            return result;
        }
    }
}
