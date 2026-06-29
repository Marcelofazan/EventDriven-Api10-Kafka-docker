using Xunit;
using Moq;
using Confluent.Kafka;
using exemploApiKafka.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace exemploApiKafka.Tests;

public class ProducerServicesTests
{
    [Fact]
    public async Task SendMessage_DeveRetornarSucesso_QuandoKafkaProcessarCorretamente()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<ProducerServices>>();
        var mockConfiguration = new Mock<IConfiguration>();
        var mockProducer = new Mock<IProducer<Null, string>>(); // Criamos o Mock do Kafka

        mockConfiguration.Setup(c => c["KafkaConfig:TopicName"]).Returns("ExemploApi");

        // Criamos o resultado falso de sucesso
        var resultFicticio = new DeliveryResult<Null, string>
        {
            Status = PersistenceStatus.Persisted,
            Topic = "ExemploApi",
            Partition = 0,
            Offset = new Offset(1)
        };

        // Configuramos o Mock para retornar o sucesso sem bater na rede
        mockProducer
            .Setup(p => p.ProduceAsync(It.IsAny<string>(), It.IsAny<Message<Null, string>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(resultFicticio);

        // Instancia passando o mock do Kafka no terceiro parâmetro
        var service = new ProducerServices(mockLogger.Object, mockConfiguration.Object, mockProducer.Object);
        string mensagemTeste = "Teste Kafka";

        // Act
        var resultado = await service.SendMessage(mensagemTeste);

        // Assert
        Assert.NotNull(resultado);
        Assert.Contains("delivered to", resultado.ToLower());
    }
}
