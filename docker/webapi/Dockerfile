FROM microsoft/aspnetcore:latest
ARG source
WORKDIR /app
COPY app/publish /app
EXPOSE 8003/tcp
ENV ASPNETCORE_URLS http://*:8003
ENV ASPNETCORE_ENVIRONMENT docker
ENV DB db
ENTRYPOINT ["dotnet", "Equinox.WebApi.dll"]