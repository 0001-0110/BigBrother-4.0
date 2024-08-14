FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy project files
COPY BigBrother.sln ./
COPY BigBrother/BigBrother.csproj ./BigBrother/
COPY RogerRoger/RogerRoger.csproj ./RogerRoger/
COPY UtilityMinistry/UtilityMinistry.csproj ./UtilityMinistry/
COPY InjectoPatronum/InjectoPatronum/InjectoPatronum.csproj ./InjectoPatronum/InjectoPatronum/
COPY Trek/Trek/Trek.csproj ./Trek/Trek/
COPY Trek/Graphium/Graphium/Graphium.csproj ./Trek/Graphium/Graphium/
# Restore dependencies
RUN dotnet restore

# Copy everything
COPY BigBrother/ ./BigBrother/
COPY RogerRoger/ ./RogerRoger/
COPY UtilityMinistry/ ./UtilityMinistry/
COPY InjectoPatronum/InjectoPatronum/ ./InjectoPatronum/InjectoPatronum/
COPY Trek/Trek/ ./Trek/Trek/
COPY Trek/Graphium/Graphium/ ./Trek/Graphium/Graphium
# Build and publish a release
RUN dotnet publish -c Release --property:PublishDir=out

# Build runtime image
FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app
# Copy the executable
COPY --from=build-env /app/BigBrother/out/ ./
# Copy the config
COPY ./BigBrotherConfig/appsettings.json ./BigBrotherConfig/
# Copy the data
COPY ./StoryVault ./StoryVault/
EXPOSE 8080
ENTRYPOINT ["dotnet", "BigBrother.dll" ]
CMD [ "./BigBrotherConfig/" ]
