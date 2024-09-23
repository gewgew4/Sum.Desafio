FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY Microservice.Common/. ./Microservice.Common/

COPY Microservice.ServiceProcessing/. ./Microservice.ServiceProcessing/

WORKDIR /app/Microservice.ServiceProcessing
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/Microservice.ServiceProcessing/out ./

# Dependencies
RUN apt-get update && apt-get install -y \
    libgdiplus \
    libc6-dev \
    libglib2.0-0 \
    libsm6 \
    libxext6 \
    libxrender1 \
    libfontconfig1 \
    tesseract-ocr \
    libtesseract-dev \
    && rm -rf /var/lib/apt/lists/*

RUN apt-get update \
    && apt-get install -y --no-install-recommends libgdiplus libc6-dev \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/*

ENTRYPOINT ["dotnet", "Microservice.ServiceProcessing.dll"]