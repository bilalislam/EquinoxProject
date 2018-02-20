FROM microsoft/aspnetcore:latest
ARG source
WORKDIR /app
EXPOSE 80
COPY app/publish /app
ENTRYPOINT ["dotnet", "Equinox.WebApi.dll"]