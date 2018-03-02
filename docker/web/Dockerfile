FROM microsoft/aspnetcore:latest
ARG source
WORKDIR /app
COPY app/publish /app
EXPOSE 8002/tcp
ENV ASPNETCORE_URLS http://*:8002
ENV ASPNETCORE_ENVIRONMENT docker
ENV DB db
ENTRYPOINT ["dotnet", "Equinox.UI.Site.dll"]