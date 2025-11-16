using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using User.Application.Interfaces;

namespace User.Infrastructure.Kafka;

public class KafkaConsumerWorker : BackgroundService
{
    private readonly ConsumerConfig _config;
    private readonly IServiceProvider _services;
    private readonly string _topic;
    public KafkaConsumerWorker(IServiceProvider services, IConfiguration cfg)
    {
        _services = services;

        _config = new ConsumerConfig
        {
            BootstrapServers = cfg["Kafka:BootstrapServers"],
            GroupId = "user-service-grp",
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = true
        };
        _topic = "order-created";
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var consumer = new ConsumerBuilder<string, string>(_config).Build();
        consumer.Subscribe(_topic);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var result = consumer.Consume(stoppingToken);
                if (result?.Message == null) continue;

                using var scope = _services.CreateScope();
                var handler = scope.ServiceProvider.GetRequiredService<IKafkaMessageHandler>();

                await handler.HandleAsync(result.Topic, result.Message.Value, stoppingToken);
            }
            catch (OperationCanceledException)
            {
                break; // graceful shutdown
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[KafkaError] {ex.Message}");
            }
        }
        consumer.Close();        
    }
}

