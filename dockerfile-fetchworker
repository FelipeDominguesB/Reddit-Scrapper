FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /App

# Copy everything
COPY . .
# Restore as distinct layers
RUN dotnet restore ./RedditScrapper.ContentFetchWorker/RedditScrapper.ContentFetchWorker.csproj
# Build and publish a release
RUN dotnet publish -c Release -o out ./RedditScrapper.ContentFetchWorker/RedditScrapper.ContentFetchWorker.csproj

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "RedditScrapper.ContentFetchWorker.dll"]