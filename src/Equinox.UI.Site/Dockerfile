FROM microsoft/aspnetcore:latest
ARG source
WORKDIR /app

EXPOSE 8002/tcp
ENV ASPNETCORE_URLS http://*:8002
ENV ASPNETCORE_ENVIRONMENT Development

COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["dotnet", "Equinox.UI.Site.dll"]