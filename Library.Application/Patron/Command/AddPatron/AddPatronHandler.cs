using Library.Application.Abstraction;
using Library.Domain.Base;
using Library.Domain.Enum;
using Library.Infrastructure.Data;
using Library.Domain.Main;
using System.Text.RegularExpressions;

namespace Library.Application.Patron.Command.AddPatron
{
    public class AddPatronHandler(DataContext context) : ICommandHandler<AddPatronCommand.Request>
    {
        public async Task<Result> Handle(AddPatronCommand.Request request, CancellationToken cancellationToken)
        {
            var result = new Result();
            result.FailIf(!Regex.IsMatch(request.Email, "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$"), ErrorKey.EmailNotValid.ToString())
                  .FailIf(request.Address != null &&
                  (string.IsNullOrEmpty(request.Address.Street) ||
                   string.IsNullOrEmpty(request.Address.City) ||
                   string.IsNullOrEmpty(request.Address.Country)), ErrorKey.SomeFieldsIsRequired.ToString());
            using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
            try
            {

                context.Patrons.Add(new PatronEntity
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    Address = request.Address,
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
