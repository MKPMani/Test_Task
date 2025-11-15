using User.Application.Responses;
using MediatR;

namespace User.Application.Queries
{
    public class GetUserByIdQuery : IRequest<UserCreatedResponse>
    {
        public string Id { get; set; }
        public GetUserByIdQuery(string id)
        {
            Id = id;
        }
    }
}
