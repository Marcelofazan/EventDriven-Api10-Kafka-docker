## 🛠️ EventDriven-Kafka
Exemplo de criação de Event-Driven Architecture (EDA) utilizando Apache Kafka em C# ASP.NET Core 10   


| Tecnologia | Descrição                  |
|----------------|-----------|
| **Kafka**      | O broker de mensagens em si. Recebe mensagens do Producer e entrega pro Consumer. |
| **Kafdrop**    | UI web pra visualizar tópicos e mensagens. |
| **API Minimal**| Abordagem simplificada e leve para construir APIs HTTP com o mínimo de arquivos, dependências e código |

#### 🔄 Executar a aplicação

| Projeto        | Descrição |
|----------------|-----------|
| **exemploKafkaAPI**  | Expõe endpoint `POST /` que envia mensagem ao Kafka. |
| **ConsumerWorkService**| Consome mensagens do tópico Kafka em background. |

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
| **POST**         | http://localhost:5107/?message=TesteKafka |
| **Kafka Cluster**| http://localhost:19000 |


#### ⚙️ Consumer — ConsumerWorkService

Worker Service que sobe um **BackgroundService (Worker)** . No **ExecuteAsync**:

- Subscreve no tópico `Exemplo-1`
- Consome mensagens em loop com `_consumer.Consume(stoppingToken)`
- Loga o conteúdo de cada mensagem recebida
- Aguarda 30 segundos entre cada iteração


