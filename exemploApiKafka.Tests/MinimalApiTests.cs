using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using exemploApiKafka.Services;
using Xunit;

namespace exemploApiKafka.Tests;

public class MinimalApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public MinimalApiTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetEndpoint_DeveRetornarOkEMensagemDoServico()
    {
        // Arrange - MUDANÇA: Agora mockamos a INTERFACE
        var mockService = new Mock<IProducerServices>();
        
        mockService
            .Setup(s => s.SendMessage("Teste Kafka"))
            .ReturnsAsync("Mensagem enviada com sucesso ao Kafka");

        var client = _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                // MUDANÇA: Substitui o registro da interface pelo mock
                services.AddTransient<IProducerServices>(_ => mockService.Object);
            });
        }).CreateClient();

        // Act
        var response = await client.GetAsync("/?message=Teste%20Kafka");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        var conteudoTexto = await response.Content.ReadAsStringAsync();
        Assert.Equal("Mensagem enviada com sucesso ao Kafka", conteudoTexto);
    }
}