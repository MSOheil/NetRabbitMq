// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using System.Text;

Console.WriteLine("Hello, World!");



var factory = new ConnectionFactory
{
    HostName = "localhost",
};


using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();


channel.QueueDeclare(queue: "inbox", durable: false, exclusive: false, autoDelete: false,
    arguments: null);

var message = " ";
while (message != String.Empty)
{


    Console.WriteLine("Message : ");

    message = Console.ReadLine();




    var body = Encoding.UTF8.GetBytes(message ?? "");
    channel.BasicPublish(exchange: "",
        routingKey: "inbox"
        , basicProperties: null,
        body: body);


    Console.WriteLine("Message sent from [Earth] to [Moon] message is : " + message);
}
