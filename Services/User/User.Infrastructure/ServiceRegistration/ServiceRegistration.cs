using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using User.Application.Interfaces;
using User.Core.Repositories;
using User.Infrastructure.Kafka;
using User.Infrastructure.Persistence;
using User.Infrastructure.Repository;

namespace User.Infrastructure.ServiceRegistration
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // Register In-Memory EF DB
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("UserDB"));

            // Register repository
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddSingleton<IKafkaProducer, KafkaProducer>();
            //services.AddHostedService<KafkaConsumerWorker>();

            return services;
        }
    }
}
