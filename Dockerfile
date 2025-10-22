FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /api

COPY *.csproj ./
RUN dotnet restore

COPY . ./

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /api
COPY --from=build /api/out .

EXPOSE 5050

ENTRYPOINT ["dotnet", "api.dll"]
