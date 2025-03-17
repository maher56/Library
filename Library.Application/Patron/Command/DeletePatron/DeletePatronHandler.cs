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

namespace Library.Application.Patron.Command.DeletePatron
{
    public class DeletePatronHandler(DataContext context) : ICommandHandler<DeletePatronCommand.Request>
    {
        public async Task<Result> Handle(DeletePatronCommand.Request request, CancellationToken cancellationToken)
        {
            var result = new Result();
            
            var patron = await context.Patrons.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            result.FailIfNullOrEmpty(patron, ErrorKey.PatronNotExist.ToString(), ResultStatus.NotFound)
                    .FailIf(await context.Borrows.AnyAsync(x => x.PatronId == request.Id && x.IsReturned == false , cancellationToken), ErrorKey.ThereAreBooksNotReturned.ToString());
            using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                patron.IsDeleted = true;
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
