# =================================================================================================
# Build Container
# =================================================================================================

# Use the .net core SDK image
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build

# Make this the current folder (Can be referenced using ./)
WORKDIR /source

# Build Speed Optimisation: Copy sln files first
# This allows the package restore to be done on a distinct layer, meaning if your dependencies don't change,
# a cached copy can be used.
# =================================================================================================
COPY *.sln ./

# Copy the csproj files
# (Note: This step assumes the project folder and project file have the same name)
COPY ./*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ./${file%.*}/ && mv $file ./${file%.*}/; done

# Restore nuget packages
RUN dotnet restore

# Copy in the source code (This command copies all files from the host to the build container)
COPY ./ ./

# Run tests
RUN dotnet test

# Publish the app to a local folder on the build container
RUN dotnet publish -c release -o /app --self-contained false --no-restore

# =================================================================================================
# App Container
# =================================================================================================

# Use the aspnet runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base

# Make this the current folder (Can be referenced using ./)
WORKDIR /app

COPY --from=build /app ./

# This is the command that the container will run on startup
ENTRYPOINT [ "dotnet","./PokeLayer.Api.dll" ]
