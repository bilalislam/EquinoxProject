FROM microsoft/aspnetcore:latest
ARG source
WORKDIR /app
EXPOSE 80
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["dotnet", "Equinox.WebApi.dll"]