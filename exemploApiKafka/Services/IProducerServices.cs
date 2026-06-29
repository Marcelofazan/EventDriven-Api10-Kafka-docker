namespace exemploApiKafka.Services;
public interface IProducerServices
{
    Task<string> SendMessage(string message);
}