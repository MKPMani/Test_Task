using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Ordering.Application.Interfaces;

namespace Ordering.Infrastructure.Kafka;

public class KafkaProducer : IKafkaProducer
{
    private readonly IProducer<string, string> _producer;

    public KafkaProducer(IConfiguration config)
    {
        var producerConfig = new ProducerConfig
        {
            BootstrapServers = config["Kafka:BootstrapServers"]
        };

        _producer = new ProducerBuilder<string, string>(producerConfig).Build();
    }

    public async Task PublishAsync<T>(string topic, T message)
    {
        var payload = JsonSerializer.Serialize(message);

        await _producer.ProduceAsync(topic, new Message<string, string>
        {
            Key = Guid.NewGuid().ToString(),
            Value = payload
        });

        _producer.Flush(TimeSpan.FromSeconds(2));
    }
}
