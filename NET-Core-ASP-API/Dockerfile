FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch
WORKDIR /app
COPY ./NET-Core-ASP-API .
RUN ["dotnet", "restore"]
RUN ["dotnet", "build"]
EXPOSE 80/tcp
RUN chmod +x ./entrypoint.sh
CMD /bin/bash ./entrypoint.sh