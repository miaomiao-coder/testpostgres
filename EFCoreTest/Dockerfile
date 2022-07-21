#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["EFCoreTest/EFCoreTest.csproj", "EFCoreTest/"]
COPY ["Service/Service.csproj", "Service/"]
COPY ["Model/Model.csproj", "Model/"]
COPY ["Repository/Repository.csproj", "Repository/"]
RUN dotnet restore "EFCoreTest/EFCoreTest.csproj"
COPY . .
WORKDIR "/src/EFCoreTest"
RUN dotnet build "EFCoreTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EFCoreTest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EFCoreTest.dll"]