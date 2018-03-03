FROM microsoft/aspnetcore:latest
ARG source
WORKDIR /app
COPY app/publish /app
EXPOSE 8003/tcp
ENV ASPNETCORE_URLS http://*:8003
ENV ASPNETCORE_ENVIRONMENT Development
ENTRYPOINT ["dotnet", "Equinox.WebApi.dll"]