using User.Application.Interfaces;

namespace User.Application.Kafka;

/// <summary>
/// Handles domain logic for Kafka messages.
/// Why: Keeps domain/business logic out of infrastructure.
/// </summary>
public sealed class KafkaMessageHandler : IKafkaMessageHandler
{
    public Task HandleAsync(string topic, string message, CancellationToken ct)
    {
        // domain logic goes here
        Console.WriteLine($"[DOMAIN] Topic={topic}, Message={message}");
        return Task.CompletedTask;
    }
}