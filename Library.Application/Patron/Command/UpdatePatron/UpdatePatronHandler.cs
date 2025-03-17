using Library.Application.Abstraction;
using Library.Domain.Base;
using Library.Domain.Enum;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library.Application.Patron.Command.UpdatePatron
{
    public class UpdatePatronHandler(DataContext context) : ICommandHandler<UpdatePatronCommand.Request>
    {
        public async Task<Result> Handle(UpdatePatronCommand.Request request, CancellationToken cancellationToken)
        {
            var result = new Result();
            var patron = await context.Patrons.FirstOrDefaultAsync(x => x.Id == request.Id , cancellationToken);
            result.FailIfNullOrEmpty(patron, ErrorKey.PatronNotExist.ToString(), ResultStatus.NotFound)
                  .FailIf(!Regex.IsMatch(request.Email, "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$"), ErrorKey.EmailNotValid.ToString())
                  .FailIf(request.Address != null &&
                  (string.IsNullOrEmpty(request.Address.Street) ||
                   string.IsNullOrEmpty(request.Address.City) ||
                   string.IsNullOrEmpty(request.Address.Country)), ErrorKey.SomeFieldsIsRequired.ToString());
            using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                patron.Name = request.Name;
                patron.Email = request.Email;
                patron.PhoneNumber = request.PhoneNumber;
                patron.Address = request.Address;
                patron.UpdatedAt = DateTime.UtcNow;
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
