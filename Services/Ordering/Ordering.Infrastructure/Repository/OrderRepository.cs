using Microsoft.EntityFrameworkCore;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;
using Ordering.Infrastructure.Persistence;

namespace Ordering.Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public AppDbContext _context { get; }
        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        async Task<Order> IOrderRepository.GetOrder(Guid id)
        {
            var res = await _context.Orders.Where(e => e.Id == id).FirstOrDefaultAsync();
            return res ?? new Order();
        }

        async Task<Order> IOrderRepository.CreateOrder(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

    }
}
