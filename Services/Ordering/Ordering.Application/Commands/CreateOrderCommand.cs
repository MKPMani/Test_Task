using MediatR;
using Ordering.Application.Responses;

namespace Ordering.Application.Commands
{
    public class CreateOrderCommand : IRequest<Guid>
    {
        public string UserId { get; set; } = string.Empty;
        public string Product { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
