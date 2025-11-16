using MediatR;
using Ordering.Application.Responses;

namespace Ordering.Application.Queries
{
    public class GetOrderByIdQuery : IRequest<OrderResponse>
    {
        public Guid Id { get; set; }
        public GetOrderByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
