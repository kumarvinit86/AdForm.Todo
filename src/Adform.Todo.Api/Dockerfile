#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Adform.Todo.Api/Adform.Todo.Api.csproj", "Adform.Todo.Api/"]
COPY ["Adform.Todo.Wireup/Adform.Todo.Wireup.csproj", "Adform.Todo.Wireup/"]
COPY ["Adform.Todo.Manager/Adform.Todo.Manager.csproj", "Adform.Todo.Manager/"]
COPY ["Adform.Todo.DomainService.Default/Adform.Todo.DomainService.Default.csproj", "Adform.Todo.DomainService.Default/"]
COPY ["Adform.Todo.DomainService/Adform.Todo.DomainService.csproj", "Adform.Todo.DomainService/"]
COPY ["Adform.Todo.Database.Sql/Adform.Todo.Database.Sql.csproj", "Adform.Todo.Database.Sql/"]
COPY ["Adform.Todo.Database/Adform.Todo.Database.csproj", "Adform.Todo.Database/"]
COPY ["Adform.Todo.Model/Adform.Todo.Model.csproj", "Adform.Todo.Model/"]
COPY ["Adform.Todo.Dto/Adform.Todo.Dto.csproj", "Adform.Todo.Dto/"]
COPY ["SeriLogger/SeriLogger.DbLogger.csproj", "SeriLogger/"]
COPY ["Adform.Todo.Manager.Default/Adform.Todo.Manager.Default.csproj", "Adform.Todo.Manager.Default/"]
COPY ["Adform.Todo.Essentials/Adform.Todo.Essentials.csproj", "Adform.Todo.Essentials/"]
RUN dotnet restore "Adform.Todo.Api/Adform.Todo.Api.csproj"
COPY . .
WORKDIR "/src/Adform.Todo.Api"
RUN dotnet build "Adform.Todo.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Adform.Todo.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Adform.Todo.Api.dll"]