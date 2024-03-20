# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY SWECVI.Database/*.csproj ./SWECVI.Database/
COPY SWECVI.ApplicationCore/*.csproj ./SWECVI.ApplicationCore/
COPY SWECVI.Infrastructure/*.csproj ./SWECVI.Infrastructure/
COPY SWECVI.Web/*.csproj ./SWECVI.Web/
RUN dotnet restore SWECVI.Web/SWECVI.Web.csproj -r linux-x64

# copy everything else and build app
COPY SWECVI.Database/ ./SWECVI.Database/
COPY SWECVI.ApplicationCore/ ./SWECVI.ApplicationCore/
COPY SWECVI.Infrastructure/ ./SWECVI.Infrastructure/
COPY SWECVI.Web/ ./SWECVI.Web/

RUN apt-get update && apt-get install -y curl
RUN curl -sL https://deb.nodesource.com/setup_14.x | bash -
RUN apt-get update && apt-get install -y nodejs
ARG buildtime_env
ENV ASPNETCORE_ENVIRONMENT $buildtime_env 
WORKDIR /source/SWECVI.Web
RUN dotnet publish -c release -o /app -r linux-x64

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app ./
ENV ASPNETCORE_ENVIRONMENT $buildtime_env
ENTRYPOINT ["dotnet", "SWECVI.Web.dll"]
