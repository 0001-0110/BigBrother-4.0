FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy project files
COPY BigBrother.sln ./
COPY BigBrother/BigBrother.csproj ./BigBrother/
COPY InjectoPatronum/InjectoPatronum/InjectoPatronum.csproj ./InjectoPatronum/InjectoPatronum/
# Restore dependencies
RUN dotnet restore

# Copy everything
COPY BigBrother/ ./BigBrother/
COPY InjectoPatronum/InjectoPatronum/ ./InjectoPatronum/InjectoPatronum/
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app
COPY --from=build-env /app/out .
COPY ./BigBrother/appsettings.json ./
EXPOSE 8080
ENTRYPOINT ["dotnet", "BigBrother.dll" ]
CMD [ "./" ]
