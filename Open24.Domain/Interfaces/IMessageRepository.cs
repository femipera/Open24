namespace Open24.Domain.Interfaces;

public interface IMessageRepository
{
    Task SendMessageAsync<T>(string queueName, T message);
    Task<T?> ReceiveMessageAsync<T>(string queueName);
}
