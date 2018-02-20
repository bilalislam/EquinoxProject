FROM microsoft/aspnetcore:latest
ARG source
WORKDIR /app
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["dotnet", "Equinox.WebApi.dll"]