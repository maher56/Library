using Library.Application.Account.Command;
using Library.Domain.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController(ISender sender) : ControllerBase
    {
        [HttpPut]
        public async Task<IActionResult> Login(LoginCommand.Request request)
        {
            var result = await sender.Send(request);
            return result.GetResult();
        }
    }
}
