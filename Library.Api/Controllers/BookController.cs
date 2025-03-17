using Library.Application.Book.Command.Addbook;
using Library.Application.Book.Command.DeleteBook;
using Library.Application.Book.Command.UpdateBook;
using Library.Application.Book.Query.GetAllBooks;
using Library.Application.Book.Query.GetBookById;
using Library.Application.Patron.Query.GetPatronById;
using Library.Domain.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [Route("api/books")]
    [ApiController]
    [Authorize]
    public class BookController(ISender sender) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var result = await sender.Send(new GetAllBooksQuery.Request());
            return result.GetResult();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(Guid id)
        {
            var result = await sender.Send(new GetBookByIdQuery.Request(id));
            return result.GetResult();
        }
        [HttpPost]
        public async Task<IActionResult> AddBook(AddbookCommand.Request request)
        {
            var result = await sender.Send(request);
            return result.GetResult();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(Guid id,UpdateBookCommand.Request request)
        {
            var result = await sender.Send(request with { Id = id});
            return result.GetResult();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            var result = await sender.Send(new DeleteBookCommand.Request(id));
            return result.GetResult();
        }
    }
}
