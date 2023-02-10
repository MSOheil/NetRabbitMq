// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory=new ConnectionFactory() { HostName="localhost"};

using (var connection = factory.CreateConnection())
using(var channel=connection.CreateModel())
{
    channel.ExchangeDeclare("logs", ExchangeType.Fanout);


    channel.QueueDeclare("b", durable: false, exclusive: false, autoDelete: false, null);


    channel.QueueDeclare("c", durable: false, exclusive: false, autoDelete: false, null);



    channel.QueueBind("b", "logs", routingKey: "");
    channel.QueueBind("c", "logs", routingKey: "");


    var message = "hello from moongs";
    var body=Encoding.UTF8.GetBytes(message);


    channel.BasicPublish("logs","",basicProperties:null,body);

    Console.WriteLine(" [x] Send from {0}", message);
}
Console.WriteLine("Please enter to exit");
Console.ReadKey();



//Console.WriteLine("Hello, World!");


//var factory = new ConnectionFactory
//{
//    HostName = "localhost",
//};


//using var connection = factory.CreateConnection();
//using var channel = connection.CreateModel();



//channel.QueueDeclare(queue: "fanout-queue", durable: false, exclusive: false, autoDelete: false,
//    arguments: null);

//var consumer = new EventingBasicConsumer(channel);
//consumer.Received += Consumer_Received;

//channel.BasicConsume(queue: "fanout-queue", true, consumer: consumer);
//Console.ReadLine();
//static void Consumer_Received(object sender, BasicDeliverEventArgs e)
//{
//    var message = Encoding.UTF8.GetString(e.Body.ToArray());

//    Console.WriteLine(message);
//}