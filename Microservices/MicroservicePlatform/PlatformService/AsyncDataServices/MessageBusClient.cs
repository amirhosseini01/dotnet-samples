using System.Text;
using System.Text.Json;
using PlatformService.Dtos;
using RabbitMQ.Client;

namespace PlatformService.AsyncDataServices;

public class MessageBusClient : IMessageBusClient
{
    private readonly IConfiguration _configuration;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    public MessageBusClient(IConfiguration configuration)
    {
        _configuration = configuration;

        var factory = new ConnectionFactory()
        {
            HostName = _configuration["RabbitMQHost"]
        };
        try
        {
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
            Console.WriteLine("--> Connected To Message Bus");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"--> Could not connect to the Message Bus {ex.Message}  {ex.InnerException?.Message}");
        }
    }

    public void PublishNewPlatform(PlatformPublishedDto platformPublishedDto)
    {
        if (!_connection.IsOpen)
        {
            Console.WriteLine("--> RabbitMQ Is Not Open, Check This Problem");
            return;
        }
        string messageObj = JsonSerializer.Serialize(platformPublishedDto);
        Console.WriteLine("--> RabbitMQ Is Open, Send Message ...");
        SendMessage(messageObj);
    }

    private void SendMessage(string messageObj)
    {
        byte[] body = Encoding.UTF8.GetBytes(messageObj);
        _channel.BasicPublish(exchange: "trigger",
                            routingKey: "",
                            basicProperties: null,
                            body: body);
        Console.WriteLine("new Message Was Sended");
    }
    public void Dispose()
    {
        Console.WriteLine("MessageBus Disposed");
        if (_channel.IsOpen)
        {
            _channel.Close();
            _connection.Close();
        }
    }

    private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
    {
        Console.WriteLine("--> RabbitMQ Connection Shutdown");
    }
}