# https://blog.nimbleways.com/docker-build-caching-for-dotnet-applications-done-right-with-dotnet-subset/
# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS prepare-restore-files
ENV PATH="${PATH}:/root/.dotnet/tools"
RUN dotnet tool install --global --no-cache dotnet-subset --version 0.3.2
WORKDIR /src
COPY . .
RUN dotnet subset restore "SaveMeter.Api.sln" --root-directory /src --output /src/restore_subset/

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /src
COPY --from=prepare-restore-files /src/restore_subset .
RUN dotnet restore "SaveMeter.Api.sln"

COPY . ./
RUN dotnet publish --no-restore -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /src
COPY --from=build-env /src/out .

ENTRYPOINT ["dotnet", "SaveMeter.Bootstrapper.dll"]
