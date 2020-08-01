FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS img-build

WORKDIR /app

COPY ./ServerHubJogoMVC ./
RUN dotnet restore
RUN dotnet publish -c Debug -o out

FROM mcr.microsoft.com/dotnet/core/runtime:3.1
WORKDIR /app

COPY --from=img-build /app/out .
ENTRYPOINT ["dotnet", "ServerHubJogoMVC.dll"]
