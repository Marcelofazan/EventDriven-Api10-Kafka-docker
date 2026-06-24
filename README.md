## 🔌 EventDriven-Kafka-Api10-docker
Exemplo de comunicação API Event-Driven Architecture por mensageria com Apache Kafka em C# ASP.NET Core 10.   

#### 📋 O que você vai encontrar neste projeto

| Tecnologia | Descrição     |
|----------------|-----------|
| **API Minimal**| Abordagem simplificada e leve para construir APIs HTTP com o mínimo de arquivos, dependências e código |
| **Event-Driven Architecture**|Design de software onde os componentes reagem a eventos (mudanças de estado), utilizando mensageria para funcionar de forma desacoplada. |
| **Kafka**      | O broker de mensagens em si. Recebe mensagens do Producer e entrega pro Consumer. |
| **Kafdrop**    | UI web pra visualizar tópicos e mensagens. |

#### 💬 Requisitos do Projeto
- Necessário **Docker** instalado.

#### 🔄 Executar a aplicação

VSCode Terminal [1]
Criar Container 
```bash
docker-compose up --build 
```

VSCode Terminal [2]
```bash
dotnet build 
cd exemploApiKafka
dotnet run 
```

Remover Container 
```bash
docker compose down --remove-orphans
```

| Host           | URL       |
|----------------|-----------|
| **Kafka Cluster**| http://localhost:19000 |
| **GET**    |http://localhost:5107/?message=Teste%20Kafka |

#### Separação de responsabilidades.

| Projeto        | Descrição |
|----------------|-----------|
| **ConsumerWorkService**| Consome mensagens do tópico Kafka em background. |
| **exemploKafkaAPI**  | Expõe endpoint `GET /` que envia mensagem ao Kafka. |

- Internamente usa `ProducerServices`, que cria um `IProducer<Null, string>` via `ProducerBuilder` e chama `ProduceAsync` no tópico configurado.

#### Fluxo de dados
Worker Service que sobe um **BackgroundService (Worker)** . No **ExecuteAsync**:

- Subscreve no tópico `Exemplo-1`
- Consome mensagens em loop com `_consumer.Consume(stoppingToken)`
- Loga o conteúdo de cada mensagem recebida
- Aguarda 30 segundos entre cada iteração
