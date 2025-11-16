using User.Application.Responses;
using User.Core.Entities;
using MediatR;

namespace User.Application.Commands
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Email { get; set; }        
    }
}
