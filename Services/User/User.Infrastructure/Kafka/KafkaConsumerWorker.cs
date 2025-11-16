using Amazon.Runtime.Internal.Util;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using User.Application.Handlers;

namespace User.Infrastructure.Kafka;

public class KafkaConsumerWorker //: BackgroundService
{
    private readonly IConfiguration _config;
    private readonly ILogger<KafkaConsumerWorker> _logger;

    public KafkaConsumerWorker(IConfiguration config, ILogger<KafkaConsumerWorker> logger)
    {
        _config = config;
        _logger = logger;
    }

    /*
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = _config["Kafka:BootstrapServers"],
            GroupId = "user-service-grp",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        var consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
        consumer.Subscribe("order-created");

        return Task.Run(() =>
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var result = consumer.Consume(stoppingToken);

                var payload = JsonSerializer.Deserialize<object>(result.Message.Value);
                Console.WriteLine($"Order created event consumed: {result.Message.Value}");
                _logger.LogInformation($"Order message {payload} successfully received");
            }
        }, stoppingToken);
    }*/
}

