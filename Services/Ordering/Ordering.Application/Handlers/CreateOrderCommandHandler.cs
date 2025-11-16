using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands;
using Ordering.Application.Interfaces;
using Ordering.Application.Mappers;
using Ordering.Application.Responses;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;
using static System.Net.WebRequestMethods;

namespace Ordering.Application.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<CreateOrderCommandHandler> _logger;
        private readonly IKafkaProducer _kafka;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, ILogger<CreateOrderCommandHandler> logger,
            IKafkaProducer kafka)
        {
            _orderRepository = orderRepository;
            _logger = logger;
            _kafka = kafka;
        }
        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = OrderMapper.Mapper.Map<Order>(request);
            if (order is null)
            {
                throw new ApplicationException("There is an issue with mapping while creating new order");
            }

            var generatedOrder = await _orderRepository.CreateOrder(order);
            _logger.LogInformation($"Order with Id {generatedOrder.Id} successfully created");

            // Publish UserCreated event
            await _kafka.PublishAsync("order-created", order);
            _logger.LogInformation($"Order created & event published for Order Id - {generatedOrder.Id}");

            var orderResponse = OrderMapper.Mapper.Map<OrderResponse>(generatedOrder);
            
            return orderResponse.Id;
        }
    }
}
