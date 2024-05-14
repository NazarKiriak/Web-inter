FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /src
COPY ["LR14/Lab14.csproj", "LR14/"]
RUN dotnet restore "LR14/Lab14.csproj"
COPY . .
WORKDIR "/src/LR14"
RUN dotnet build "Lab14.csproj" -c Release -o /app/build

RUN dotnet add package ClosedXML

FROM build AS publish
RUN dotnet publish "Lab14.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lab14.dll"]