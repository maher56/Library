using Library.Application.Borrow.Command.BorrowBook;
using Library.Application.Borrow.Command.ReturnBook;
using Library.Domain.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class BorrowController(ISender sender) : ControllerBase
    {
        [HttpPost("borrow/{bookId}/patron/{patronId}")]
        public async Task<IActionResult> BorrowBook(Guid bookId,Guid patronId)
        {
            var result = await sender.Send(new BorrowBookCommand.Request(patronId,bookId));
            return result.GetResult();
        }
        [HttpPut("borrow/{bookId}/patron/{patronId}")]
        public async Task<IActionResult> ReturnBook(Guid bookId, Guid patronId)
        {
            var result = await sender.Send(new ReturnBookCommand.Request(patronId,bookId));
            return result.GetResult();
        }
    }
}
