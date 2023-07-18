using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://lmbmlcrb:WPOsx0guESB6QeQT6X6oLXwewqa2QmTn@vulture.rmq.cloudamqp.com/lmbmlcrb");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "fanout-exchange-example", type: ExchangeType.Fanout);

for (int i = 0; i < 20; i++)
{
    byte[] messagge = Encoding.UTF8.GetBytes($"Merhaba" + i);
    channel.BasicPublish(exchange: "fanout-exchange-example", routingKey: string.Empty, body: messagge);

}

Console.Read();