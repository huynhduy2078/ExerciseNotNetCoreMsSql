FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["ExerciseOne/ExerciseOne.csproj", "ExerciseOne/"]
RUN dotnet restore "ExerciseOne/ExerciseOne.csproj"
COPY . .
WORKDIR "/src/ExerciseOne"
RUN dotnet build "ExerciseOne.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ExerciseOne.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExerciseOne.dll"]