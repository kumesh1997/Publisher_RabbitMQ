namespace TodoApi.Services;

public interface IMessageProducer{
    // Abstract Method
    public void SendingMessage<T>(T message);
}