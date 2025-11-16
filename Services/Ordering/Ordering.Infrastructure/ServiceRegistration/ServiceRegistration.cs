using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Ordering.Infrastructure.Persistence;
using Ordering.Core.Repositories;
using Ordering.Infrastructure.Repository;
using Ordering.Application.Interfaces;
using Ordering.Infrastructure.Kafka;
using Ordering.Application.Kafka;

namespace Ordering.Infrastructure.ServiceRegistration
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // Register In-Memory EF DB
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("OrderDB"));

            // Register repository
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddSingleton<IKafkaProducer, KafkaProducer>();
            services.AddSingleton<IKafkaMessageHandler, KafkaMessageHandler>();
            services.AddHostedService<KafkaConsumerWorker>();

            return services;
        }
    }
}
