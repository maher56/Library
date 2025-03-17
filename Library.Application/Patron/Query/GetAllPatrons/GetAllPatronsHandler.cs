using Library.Application.Abstraction;
using Library.Domain.Base;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Patron.Query.GetAllPatrons
{
    public class GetAllPatronsHandler(DataContext context) : IQueryHandler<GetAllPatronsQuery.Request, List<GetAllPatronsQuery.Response>>
    {
        public async Task<Result<List<GetAllPatronsQuery.Response>>> Handle(GetAllPatronsQuery.Request request, CancellationToken cancellationToken)
        {
            var result = new Result<List<GetAllPatronsQuery.Response>>();
            result.Data = await context.Patrons
                .AsNoTracking()
                .OrderBy(x => x.CreatedAt)
                .Select(x => new GetAllPatronsQuery.Response
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                }).ToListAsync(cancellationToken);
            return result;
        }
    }
}
