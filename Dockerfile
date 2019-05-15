FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build

COPY . /app

WORKDIR /app

RUN ["dotnet", "restore"]
RUN ["dotnet", "build"]
RUN ["dotnet", "publish", "-o", "out"]

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

ENTRYPOINT [ "dotnet", "OnBoard.dll" ]




