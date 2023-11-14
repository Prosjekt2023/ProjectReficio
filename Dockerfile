#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
# Stage 1: Base Image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
# Expose port 80 for HTTP
EXPOSE 80
# Expose port 443 for HTTPS  
EXPOSE 443 

# Stage 2: Build Image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
# Copy the project file to the current directory
COPY ["ReficioSolution.csproj", "."]
RUN dotnet restore "./ReficioSolution.csproj" # Restore dependencies
# Copy the entire application code
COPY . . 
WORKDIR "/src/."
RUN dotnet build "ReficioSolution.csproj" -c Release -o /app/build # Build the application in Release mode

# Stage 3: Publish Image
FROM build AS publish
RUN dotnet publish "ReficioSolution.csproj" -c Release -o /app/publish /p:UseAppHost=false # Publish the application

# Stage 4: Final Image
FROM base AS final
WORKDIR /app
# Copy published application from the 'publish' stage
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReficioSolution.dll", "--environment=Development"]
# Set the default command to run when the container starts with the application in Development environment
