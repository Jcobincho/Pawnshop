FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Pawnshop.Api/Pawnshop.Api.csproj", "Pawnshop.Api/"]
COPY ["Pawnshop.Domain/Pawnshop.Domain.csproj", "Pawnshop.Domain/"]
COPY ["Pawnshop.Application/Pawnshop.Application.csproj", "Pawnshop.Application/"]
COPY ["Pawnshop.Infrastructure/Pawnshop.Infrastructure.csproj", "Pawnshop.Infrastructure/"]
RUN dotnet restore "Pawnshop.Api/Pawnshop.Api.csproj"
COPY . .
WORKDIR "/src/Pawnshop.Api"
RUN dotnet build "Pawnshop.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Pawnshop.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

USER root
RUN apt-get update && apt-get install -y openssl
RUN openssl req -x509 -newkey rsa:4096 -keyout /app/key.pem -out /app/cert.pem -days 365 -nodes -subj '/CN=localhost'
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/app/cert.pem
ENV ASPNETCORE_Kestrel__Certificates__Default__KeyPath=/app/key.pem
ENTRYPOINT ["dotnet", "Pawnshop.Api.dll"]
