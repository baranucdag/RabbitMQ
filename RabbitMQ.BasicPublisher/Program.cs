//bağlantıyı oluşturma (rabbitMQ sunucusuna bağlantı)
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://lmbmlcrb:WPOsx0guESB6QeQT6X6oLXwewqa2QmTn@vulture.rmq.cloudamqp.com/lmbmlcrb");

//bağlantının aktifleştirilmesi ve kanalın oluşturulması
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.QueueDeclare(queue: "example-queue", exclusive: false, durable: true);

//Queue'den mesaj okuma (kuyruktaki mesajları okuma)
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: "example-queue", autoAck: false, consumer: consumer);
consumer.Received += (sender, e) =>
{
    //kuyruğa gelen mesajın işlenmesi
    //e.Body(): kuyruktaki mesajın verisini bütünsel olarak getirecektir.
    //e.Body.Span veya e.Body.ToArray(): kuyruktaki mesajın byte verisine getirecektir.
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));

    channel.BasicAck(deliveryTag: e.DeliveryTag, multiple: false);
};

Console.Read();