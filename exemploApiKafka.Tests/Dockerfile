# Estágio de compilação
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# 1. Copia os arquivos de projeto (.csproj) para restaurar os pacotes (Aproveita o Cache do Docker)
COPY ["exemploApiKafka/exemploApiKafka.csproj", "exemploApiKafka/"]
COPY ["exemploApiKafka.Tests/exemploApiKafka.Tests.csproj", "exemploApiKafka.Tests/"]
RUN dotnet restore "exemploApiKafka.Tests/exemploApiKafka.Tests.csproj"

# 2. Copia o restante de todo o código fonte
COPY . .

# 3. Muda para o diretório do projeto de testes
WORKDIR "/src/exemploApiKafka.Tests"

# 4. Define que o container vai rodar os testes assim que inicializar
CMD ["dotnet", "test", "--no-restore", "--verbosity", "normal"]