using RabbitMQ.Client;

ConnectionFactory factory = new();
factory.Uri = new("amqps://lmbmlcrb:WPOsx0guESB6QeQT6X6oLXwewqa2QmTn@vulture.rmq.cloudamqp.com/lmbmlcrb");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "fanout-exchange-example", type: ExchangeType.Fanout);

Console.Write("Kuyruk adını giriniz:");

Console.Read();