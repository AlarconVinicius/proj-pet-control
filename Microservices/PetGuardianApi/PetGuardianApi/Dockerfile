FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Microservices/PetGuardianApi/PetGuardianApi/PetGuardianApi.csproj", "Microservices/PetGuardianApi/PetGuardianApi/"]
RUN dotnet restore "Microservices/PetGuardianApi/PetGuardianApi/PetGuardianApi.csproj"
COPY . .
WORKDIR "/src/Microservices/PetGuardianApi/PetGuardianApi"
RUN dotnet build "PetGuardianApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PetGuardianApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PetGuardianApi.dll"]