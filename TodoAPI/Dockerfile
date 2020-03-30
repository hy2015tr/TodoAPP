#-------------------BASE--------------------------------------------#

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
ENV ASPNETCORE_ENVIRONMENT "Development"
ENV ASPNETCORE_URLS "http://+:5000"
EXPOSE 5000
WORKDIR /app

#-------------------BUILD-------------------------------------------#

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["TodoAPI.csproj", "./"]
RUN dotnet restore "./TodoAPI.csproj"
COPY . .
WORKDIR /src/.
RUN dotnet build "TodoAPI.csproj" -c Release -o /app/build

#-------------------PUBLISH-----------------------------------------#

FROM build AS publish
RUN dotnet publish "TodoAPI.csproj" -c Release -o /app/publish

#-------------------FINAL-------------------------------------------#

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TodoAPI.dll"]

#-------------------------------------------------------------------#