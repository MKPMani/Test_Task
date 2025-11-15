using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace User.Infrastructure.Kafka;

public class KafkaConsumerWorker : BackgroundService
{
    private readonly IConfiguration _config;

    public KafkaConsumerWorker(IConfiguration config)
    {
        _config = config;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = _config["Kafka:BootstrapServers"],
            GroupId = "user-service",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        var consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
        consumer.Subscribe("user-created");

        /*while (!stoppingToken.IsCancellationRequested)
        {
            var msg = consumer.Consume(stoppingToken);
            Console.WriteLine(msg.Message.Value);
        }*/

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
}

