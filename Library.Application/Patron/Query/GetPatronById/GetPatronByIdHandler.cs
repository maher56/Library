
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

namespace Library.Application.Patron.Query.GetPatronById
{
    public class GetPatronByIdHandler(DataContext context) : IQueryHandler<GetPatronByIdQuery.Request, GetPatronByIdQuery.Response>
    {
        public async Task<Result<GetPatronByIdQuery.Response>> Handle(GetPatronByIdQuery.Request request, CancellationToken cancellationToken)
        {
            var result = new Result<GetPatronByIdQuery.Response>();
            result.FailIf(!await context.Patrons.AnyAsync(x => x.Id == request.Id,cancellationToken), ErrorKey.PatronNotExist.ToString(), ResultStatus.NotFound);
            result.Data = await context.Patrons
                .AsNoTracking()
                .Select(x => new GetPatronByIdQuery.Response
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    Address = x.Address,
                    MembershipDate = x.CreatedAt,
                }).FirstAsync(x => x.Id == request.Id , cancellationToken);
            return result;
        }
    }
}
