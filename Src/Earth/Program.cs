// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;







var factory = new ConnectionFactory() { HostName = "localhost" };

using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    Console.WriteLine(" [*] Waiting for logs");

    var consumer=new EventingBasicConsumer(channel);

    consumer.Received += (model, ea) =>
    {
        byte[] body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine(" [x] {0}",message);
    };

    channel.BasicConsume("b", autoAck: true, consumer: consumer);

    Console.ReadLine();

}


//Console.WriteLine("Hello, World!");



//var factory = new ConnectionFactory
//{
//    HostName = "localhost",
//};


//using var connection = factory.CreateConnection();
//using var channel = connection.CreateModel();

//channel.ExchangeDeclare("fanout-exchange", ExchangeType.Fanout);
//channel.QueueDeclare("fanout-queue", durable: true, exclusive: true, autoDelete: false, arguments: null);


//channel.QueueBind("fanout-queue", "fanout-exchange", String.Empty);
//channel.BasicQos(0, 10, false);


//var consumer = new EventingBasicConsumer(channel);
//consumer.Received += (sender, args) =>
//{
//    var body = args.Body.ToArray();
//    var message = Encoding.UTF8.GetString(body);
//    Console.WriteLine(message);
//};

//channel.BasicConsume("fanout-queue", true, consumer);
//Console.WriteLine("Consumer Started");
//Console.ReadKey();

//channel.QueueDeclare(queue: "inbox", durable: false, exclusive: false, autoDelete: false,
//    arguments: null);

//var message = " ";
//while (message != String.Empty)
//{


//    Console.WriteLine("Message : ");

//    message = Console.ReadLine();




//    var body = Encoding.UTF8.GetBytes(message ?? "");
//    channel.BasicPublish(exchange: "",
//        routingKey: "inbox"
//        , basicProperties: null,
//        body: body);


//    Console.WriteLine("Message sent from [Earth] to [Moon] message is : " + message);
//}
