using User.Application.Responses;
using MediatR;

namespace User.Application.Queries
{
    public class GetUserByIdQuery : IRequest<UserCreatedResponse>
    {
        public Guid Id { get; set; }
        public GetUserByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
