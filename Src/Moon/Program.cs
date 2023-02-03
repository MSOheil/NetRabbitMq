// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
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

var consumer = new EventingBasicConsumer(channel);
consumer.Received += Consumer_Received;

channel.BasicConsume(queue: "inbox", true, consumer: consumer);
Console.ReadLine();
static void Consumer_Received(object sender, BasicDeliverEventArgs e)
{
    var message = Encoding.UTF8.GetString(e.Body.ToArray());

    Console.WriteLine(message);
}