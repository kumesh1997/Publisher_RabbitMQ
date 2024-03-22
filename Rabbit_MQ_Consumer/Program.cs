using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;


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
channel.QueueDeclare("todos", durable: true, exclusive: false);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, eventArgs) => {
    // getting byte[]
    var body = eventArgs.Body.ToArray();
    // convert byte[]
    var message = Encoding.UTF8.GetString(body);

    Console.WriteLine($"A message has been got {message}");
};

channel.BasicConsume("todos", autoAck: true, consumer: consumer);

Console.ReadKey();





// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.

// builder.Services.AddControllers();
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();

// app.UseAuthorization();

// app.MapControllers();

// app.Run();
