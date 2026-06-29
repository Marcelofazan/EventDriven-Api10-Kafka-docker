using exemploApiKafka.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IProducerServices, ProducerServices>();

var app = builder.Build();

// MUDANÇA AQUI: Altere o tipo injetado no endpoint para IProducerServices
app.MapGet("/", async ([FromServices] IProducerServices services, [FromQuery] string message) =>
{
    return await services.SendMessage(message);
});

app.Run();