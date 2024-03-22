namespace TodoApi.Services;

using System.Text;
using System.Text.Json;
using RabbitMQ.Client;


public class MessageProducer : IMessageProducer
{
    // Override the Abstract Method
    public void SendingMessage<T>(T message)
    {
        // Configurations
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "user",
            Password = "mypassword",
            VirtualHost = "/"
        };
        // Create a Connection from Configurations
        var con = factory.CreateConnection();
        // Specify the Channel
        using var channel = con.CreateModel();
        // Create a Queue
        channel.QueueDeclare( "todos", durable: true, exclusive: false);

        var jsonmessage = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(jsonmessage);
        // Publish the Message
        channel.BasicPublish(exchange: "", routingKey: "todos", body: body);
    }
}