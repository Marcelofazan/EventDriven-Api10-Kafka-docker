using Confluent.Kafka;

namespace exemploApiKafka.Services;

public class ProducerServices : IProducerServices
{
    private readonly ILogger<ProducerServices> _logger;
    private readonly IConfiguration _configuration;
    private readonly IProducer<Null, string>? _producer;

    // Construtor usado em Produção (Injeta as coisas automaticamente)
    public ProducerServices(ILogger<ProducerServices> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    // CONSTRUTOR ADICIONADO PARA TESTES UNITÁRIOS (Aceita o Mock do Kafka)
    public ProducerServices(ILogger<ProducerServices> logger, IConfiguration configuration, IProducer<Null, string> producer) 
        : this(logger, configuration)
    {
        _producer = producer;
    }

    public async Task<string> SendMessage(string message)
    {
        var topic = _configuration["KafkaConfig:TopicName"] ?? "ExemploApi";
        
        // Se houver um producer injetado pelo teste, usa ele. Se não, cria o real (Produção).
        using var producer = _producer ?? new ProducerBuilder<Null, string>(new ProducerConfig 
        { 
            BootstrapServers = _configuration["KafkaConfig:BootstrapServer"] 
        }).Build();

        try
        {
            var result = await producer.ProduceAsync(topic, new Message<Null, string> { Value = message });
            var returnedMessage = $"Message '{message}' delivered to '{result.TopicPartitionOffset}'";
            _logger.LogInformation(returnedMessage);
            return returnedMessage;
        }
        catch (ProduceException<Null, string> ex)
        {
            var errorMessage = $"Delivery failed: {ex.Error.Reason}";
            _logger.LogError(errorMessage);
            return errorMessage;
        }
    }
}
