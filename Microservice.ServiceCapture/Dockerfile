FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY ["Microservice.ServiceCapture/Microservice.ServiceCapture.csproj", "Microservice.ServiceCapture/"]
COPY ["Microservice.Common/Microservice.Common.csproj", "Microservice.Common/"]

RUN dotnet restore "Microservice.ServiceCapture/Microservice.ServiceCapture.csproj"

COPY . .

WORKDIR "/src/Microservice.ServiceCapture"
RUN dotnet build "Microservice.ServiceCapture.csproj" -c Release -o /app/build

RUN dotnet publish "Microservice.ServiceCapture.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/runtime:8.0

# Check dependencies
RUN apt-get update && apt-get install -y \
    libopencv-dev \
    libgdiplus \
    && rm -rf /var/lib/apt/lists/*

COPY --from=build /app/publish .

COPY --from=build /src/Microservice.Common/runtimes/linux-x64/native/libOpenCvSharpExtern.so /usr/lib/

WORKDIR /app

ENV KAFKA_BOOTSTRAP_SERVERS=localhost:9092
ENV LD_LIBRARY_PATH=/usr/lib:$LD_LIBRARY_PATH

ENTRYPOINT ["dotnet", "Microservice.ServiceCapture.dll"]
