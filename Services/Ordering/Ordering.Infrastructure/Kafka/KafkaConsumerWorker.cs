using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

namespace Ordering.Infrastructure.Kafka;

public class KafkaConsumerWorker //: BackgroundService
{
    private readonly IConfiguration _config;
    public KafkaConsumerWorker(IConfiguration config)
    {
        _config = config;
    }

    /*
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = _config["Kafka:BootstrapServers"],
            GroupId = "order-service-grp",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        var consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
        consumer.Subscribe("user-created");

        return Task.Run(() =>
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var result = consumer.Consume(stoppingToken);

                var payload = JsonSerializer.Deserialize<object>(result.Message.Value);
                Console.WriteLine($"User created event consumed: {result.Message.Value}");
            }
        }, stoppingToken);
    }
    */
}
