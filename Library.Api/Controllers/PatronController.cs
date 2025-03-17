using Library.Application.Patron.Command.AddPatron;
using Library.Application.Patron.Command.DeletePatron;
using Library.Application.Patron.Command.UpdatePatron;
using Library.Application.Patron.Query.GetAllPatrons;
using Library.Application.Patron.Query.GetPatronById;
using Library.Domain.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [Route("api/patrons")]
    [ApiController]
    [Authorize]
    public class PatronController(ISender sender) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await sender.Send(new GetAllPatronsQuery.Request());
            return result.GetResult();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatronById(Guid id)
        {
            var result = await sender.Send(new GetPatronByIdQuery.Request(id));
            return result.GetResult();
        }
        [HttpPost]
        public async Task<IActionResult> AddPatron(AddPatronCommand.Request request)
        {
            var result = await sender.Send(request);
            return result.GetResult();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatron(Guid id,UpdatePatronCommand.Request request)
        {
            var result = await sender.Send(request with { Id = id });
            return result.GetResult();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatron(Guid id)
        {
            var result = await sender.Send(new DeletePatronCommand.Request(id));
            return result.GetResult();
        }

    }
}
