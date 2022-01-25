# =================================================================================================
# Build Container
# =================================================================================================

# Use the .net core SDK image
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build

# Make this the current folder (Can be referenced using ./)
WORKDIR /source

# Build Speed Optimisation: Copy sln, Nuget config and csproj files first
# This allows the package restore to be done on a distinct layer, meaning if your dependencies don't change,
# a cached copy can be used.
# (We want to copy over all the csproj files. Unfortunately COPY doesn't support globs like this: COPY ./**/*.csproj ./)
# =================================================================================================
COPY *.sln ./

# Copy the csproj files
# (Note: This step assumes the project folder and project file have the same name)
COPY ./*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ./${file%.*}/ && mv $file ./${file%.*}/; done

# If you have project folders and name which differ, they must be manually specified like this:
#COPY ./Prefix.ProjectName1/*.csproj ./Prefix.ProjectName1/
#COPY ./Prefix.ProjectName2/*.csproj ./Prefix.ProjectName2/
#COPY ./Prefix.ProjectName3/*.csproj ./Prefix.ProjectName3/

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

# Copy the app dependencies and application separately so that they go into different layers
# (This should allow docker to cache layers more efficiently. If the dependencies didn't
#  change, only the /app layer will need to be rebuilt)
COPY --from=build /app ./

# This is the command that the container will run on startup
ENTRYPOINT [ "dotnet","./PokeLayer.Api.dll" ]
