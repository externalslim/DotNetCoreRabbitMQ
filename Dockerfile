FROM microsoft/dotnet:2.2-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src

COPY MS.Consumer/MS.Consumer.csproj MS.Consumer/
COPY MS.Logic/MS.Logic.csproj MS.Logic/
COPY MS.Data/MS.Data.csproj MS.Data/

RUN dotnet restore MS.Consumer/MS.Consumer.csproj
COPY . .
WORKDIR /src/MS.Consumer
RUN dotnet build MS.Consumer.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish MS.Consumer.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MS.Consumer.dll"]
