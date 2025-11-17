using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace User.Infrastructure.Kafka;

public class KafkaConsumerWorker : BackgroundService
{
    private readonly ILogger<KafkaConsumerWorker> _logger;
    private readonly string _topic = "order-created";
    private readonly IConsumer<string, string> _consumer;

    public KafkaConsumerWorker(ILogger<KafkaConsumerWorker> logger, IConfiguration _config)
    {
        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = _config["Kafka:BootstrapServers"],
            GroupId = "user-service-grp",
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false
        };

        _consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
        _logger = logger;
    }    
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await WaitForTopic(_topic, stoppingToken);

        _consumer.Subscribe(_topic);
        
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var result = _consumer.Consume(stoppingToken);

                _logger.LogInformation("Order consumed - message: {Message}", result.Message.Value);

                await ProcessOrder(result.Message.Value);

                _consumer.Commit(result);
            }
            catch (ConsumeException ex)
            {
                _logger.LogError(ex, "Order consume error in User Service");
            }
        }
        _consumer.Close();
    }

    private Task ProcessOrder(string value)
    {
        _logger.LogInformation("Processing order: {Value}", value);
        return Task.CompletedTask;
    }

    private async Task WaitForTopic(string topic, CancellationToken token)
    {
        var adminConfig = new AdminClientConfig
        {
            BootstrapServers = "kafka:9092"
        };

        using var admin = new AdminClientBuilder(adminConfig).Build();

        while (!token.IsCancellationRequested)
        {
            try
            {
                var metadata = admin.GetMetadata(topic, TimeSpan.FromSeconds(3));

                if (metadata.Topics.Any(t => t.Topic == topic && t.Error.Code == ErrorCode.NoError))
                {
                    _logger.LogInformation("Topic '{Topic}' is available.", topic);
                    return;
                }

                _logger.LogWarning("Topic '{Topic}' not ready yet...", topic);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Waiting for Kafka topic '{Topic}'...", topic);
            }

            await Task.Delay(2000, token); // Wait 2 secs before retry
        }
    }


}

