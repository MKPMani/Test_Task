using Ordering.Core.Entities;

namespace Ordering.Core.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetOrder(string id);
        Task<Order> CreateOrder(Order order);
        
    }
}
