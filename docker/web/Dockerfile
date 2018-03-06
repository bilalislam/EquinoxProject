FROM microsoft/aspnetcore:latest
ARG source
WORKDIR /app
COPY app/publish /app
COPY setup.sh /app
RUN chmod +x /app/setup.sh
EXPOSE 8002/tcp
ENV ASPNETCORE_URLS http://*:8002
ENV ASPNETCORE_ENVIRONMENT Development
ENTRYPOINT ["/app/setup.sh"]