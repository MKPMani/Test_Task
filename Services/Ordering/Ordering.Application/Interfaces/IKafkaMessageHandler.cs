namespace Ordering.Application.Interfaces;
public interface IKafkaMessageHandler
{
    Task HandleAsync(string topic, string message, CancellationToken ct);
}