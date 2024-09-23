FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY ["Microservice.ServiceQueue/Microservice.ServiceQueue.csproj", "Microservice.ServiceQueue/"]
COPY ["Microservice.Common/Microservice.Common.csproj", "Microservice.Common/"]

RUN dotnet restore "Microservice.ServiceQueue/Microservice.ServiceQueue.csproj"

COPY . .

WORKDIR "/src/Microservice.ServiceQueue"
RUN dotnet build "Microservice.ServiceQueue.csproj" -c Release -o /app/build

RUN dotnet publish "Microservice.ServiceQueue.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/runtime:8.0

COPY --from=build /app/publish .

WORKDIR /app

ENV KAFKA_BOOTSTRAP_SERVERS=localhost:9092

ENTRYPOINT ["dotnet", "Microservice.ServiceQueue.dll"]
