using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands;
using Ordering.Application.Interfaces;
using Ordering.Application.Mappers;
using Ordering.Application.Responses;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;

namespace Ordering.Application.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderResponse>
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
        public async Task<OrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = OrderMapper.Mapper.Map<Order>(request);
            if (order is null)
            {
                throw new ApplicationException("There is an issue with mapping while creating new order");
            }

            // Publish UserCreated event
            await _kafka.PublishAsync("order-created", order);

            var generatedOrder = await _orderRepository.CreateOrder(order);
            _logger.LogInformation($"Order with Id {generatedOrder.Id} successfully created");

            var orderResponse = OrderMapper.Mapper.Map<OrderResponse>(generatedOrder);
            orderResponse.Message = "Order created & event published";
            return orderResponse;
        }
    }
}
