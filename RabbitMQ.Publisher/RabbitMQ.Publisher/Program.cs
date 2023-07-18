using RabbitMQ.Client;
using System.Text;

//bağlantı oluşturma
ConnectionFactory factory = new();
factory.Uri = new Uri("amqps://lmbmlcrb:WPOsx0guESB6QeQT6X6oLXwewqa2QmTn@vulture.rmq.cloudamqp.com/lmbmlcrb");

//bağlantıyı aktifleştirme ve kanal açma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

// kuyruk oluşturma
channel.QueueDeclare(queue: "example-queue", exclusive: false, durable: true);

//kuyruğun kalıcı hale getirilmesi için (durable : true) (basicProperties:properties)
IBasicProperties properties = channel.CreateBasicProperties();
properties.Persistent= true;

for (int i = 0; i < 100; i++)
{
    await Task.Delay(100);
    byte[] message = Encoding.UTF8.GetBytes("Test1 " + i);
    channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message, basicProperties:properties);
}

Console.WriteLine("Hello World");

Console.Read();

#region Ders Notları

#region Genel Tanımlamalar
/*
     ****Exchange Types => Direct Exchange, Fanout Exchange ,Topic Exchange, Header Exchange

        Direct Exchange => Mesajların direkt olarak belirli bir kuyruğa iletilmesini sağlar. Mesaj routing key'e uygun olan
        hedef kuyruklara gönderilir. Bunun için mesaj gönderilecek kuruğun adını routing key olarak belirlemek yeterlidir.
        Yani burada hedef bir kuruğa gönderilecek mesajın routing key alanına kuyruk adını vermek yeterlidir.

        Fanout Exchange => Mesajların bu exchange'a bind olmuş bütün kuyruklara gönderilmesini sağlar. Publisher mesajların
        gönderildiği kuyruk isimlerini dikkate almaz ve mesajları tüm kuyruklara gönderir.
        
        Topic Exchange => Routing key'leri kullanarak   mesajları kuyruklara yönlendirmek için kullanılan exchange'dir.
        Bu exchange ile routing key'lerin bir ksımına/formatına/yapısına bakılarak kuyruklara mesaj gönderir. 

        Header Exchange => Routing key'leri kullanmak yerine header'ları kullanarak mesajları kuyruklara yönlendirmek için 
        kullanılan exchange yönetimidir.
 

    *****exclusive parametresi bu kuyruğun özel olup olmadığını birden fazla bağlantı ile bu kuyrukta işlem yapılıp yapılamayacağını belirtir. 
 */
#endregion

#region Gelişmiş kuyruk mimarisi

/*
    -----Round-Robin Dispatching => default olarak tüm consumer'lara sırasıyla mesaj gönderilir.
    
    -----Message-Acknowledgement => tüketiciye gönderdiği mesajları işlesin yada işlemesin hemen kuyruktan silinmesi üzere işaretler.
         Eğer mesaj başarılı bir şekilde işlendiyse mesjaın kuyruktan silinmesi için tüketicinin RMQ'yu uyarması gerekmektedir. Bunu yapar.    
        =>autoAck parametresi ile yapılır, mesajı kuyruktan silmek için işlemlerden sona- channel.BasicAck(deliveryTag: e.DeliveryTag, multiple: false);

    -----Message Durability => RMQ sunucusunun düşmesini ihtimaline karşı kuyruğu kalıcı olarak işaretlemek için durable parametresi kullanılır.
        -channel.QueueDeclare(queue: "example-queue", exclusive: false, durable: true);
        -
    
    ----Mesaj işleme olaylar konfigüre edilebilir (mesaj boyutu, mesaj sayısı, hedef kitle) (channel.BasicQos())
 */
#endregion
#endregion
