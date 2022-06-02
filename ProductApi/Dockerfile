FROM mcr.microsoft.com/dotnet/sdk:6.0

WORKDIR /ProductApi

RUN apt-get update -y && apt-get install socat -y

COPY ProductApi.csproj ProductApi.csproj

RUN dotnet restore

EXPOSE 5000

ENV DOTNET_USE_POLLING_FILE_WATCHER 1

COPY docker-entrypoint.sh docker-entrypoint.sh

CMD ["./docker-entrypoint.sh"]