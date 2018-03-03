FROM microsoft/aspnetcore:latest
ARG source
WORKDIR /app

EXPOSE 8003/tcp
ENV ASPNETCORE_URLS http://*:8003
ENV ASPNETCORE_ENVIRONMENT Development

COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["dotnet", "Equinox.WebApi.dll"]