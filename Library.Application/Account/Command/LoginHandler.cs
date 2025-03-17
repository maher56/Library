using Library.Application.Abstraction;
using Library.Application.Services.Token;
using Library.Domain.Base;
using Library.Domain.Enum;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Account.Command
{
    public class LoginHandler(DataContext context,ITokenService tokenService) : ICommandHandler<LoginCommand.Request, LoginCommand.Response>
    {
        public async Task<Result<LoginCommand.Response>> Handle(LoginCommand.Request request, CancellationToken cancellationToken)
        {
            var result = new Result<LoginCommand.Response>();
            var admin = await context.Admins.FirstOrDefaultAsync(x => x.Name == request.UserName,cancellationToken);
            result.FailIfNullOrEmpty(admin, ErrorKey.UserNameOrPasswordNotExist.ToString(), ResultStatus.NotFound)
                  .FailIfNullOrEmpty(!PasswordHelper.VerifyPassword(request.Password, admin.Password), ErrorKey.UserNameOrPasswordNotExist.ToString(), ResultStatus.NotFound);
            var token = await tokenService.GenerateJwtToken(admin.Id);
            result.Data = new LoginCommand.Response { Token = token};
            return result;
        }
    }
}
