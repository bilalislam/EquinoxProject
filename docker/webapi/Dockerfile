FROM microsoft/aspnetcore:latest
ARG source
WORKDIR /app
COPY app/publish /app
ENV ASPNETCORE_URLS http://*:8003
ENV ASPNETCORE_ENVIRONMENT docker
ENTRYPOINT ["dotnet", "Equinox.WebApi.dll"]