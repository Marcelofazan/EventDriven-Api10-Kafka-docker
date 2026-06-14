## 🔌 EventDriven-Kafka-Api10-docker
Exemplo de criação de Event-Driven Architecture (EDA) utilizando Apache Kafka em C# ASP.NET Core 10   

#### 📋 O que você vai encontrar neste projeto

| Tecnologia | Descrição     |
|----------------|-----------|
| **API Minimal**| Abordagem simplificada e leve para construir APIs HTTP com o mínimo de arquivos, dependências e código |
| **Kafka**      | O broker de mensagens em si. Recebe mensagens do Producer e entrega pro Consumer. |
| **Kafdrop**    | UI web pra visualizar tópicos e mensagens. |


#### 🔄 Executar a aplicação

| Projeto        | Descrição |
|----------------|-----------|
| **ConsumerWorkService**| Consome mensagens do tópico Kafka em background. |
| **exemploKafkaAPI**  | Expõe endpoint `POST /` que envia mensagem ao Kafka. |

VSCode Terminal [1]
```bash
docker-compose up --build 
```

VSCode Terminal [2]
```bash
dotnet build 
cd exemploApiKafka
dotnet run 
```

- Remover Container 
```bash
docker compose down --remove-orphans
```

- Internamente usa `ProducerServices`, que cria um `IProducer<Null, string>` via `ProducerBuilder` e chama `ProduceAsync` no tópico configurado.

| Host           | URL       |
|----------------|-----------|
| **Kafka Cluster**| http://localhost:19000 |
| **POST**         | http://localhost:5107/?message=TesteKafka |


#### ⚙️ Consumer — ConsumerWorkService

Worker Service que sobe um **BackgroundService (Worker)** . No **ExecuteAsync**:

- Subscreve no tópico `Exemplo-1`
- Consome mensagens em loop com `_consumer.Consume(stoppingToken)`
- Loga o conteúdo de cada mensagem recebida
- Aguarda 30 segundos entre cada iteração


