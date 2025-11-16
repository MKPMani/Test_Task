using User.Application.Commands;
using User.Application.Queries;
using User.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace User.API.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("[action]/{id}", Name = "GetUserById")]        
        public async Task<ActionResult<UserCreatedResponse>> GetUserById(Guid id)
        {
            var query = new GetUserByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }        

        [HttpPost]
        [Route("CreateUser")]
        public async Task<ActionResult<Guid>> CreateUser([FromBody] CreateUserCommand userCommand)
        {
            var result = await _mediator.Send(userCommand);
            return Ok(result);
        }
        
    }
}
