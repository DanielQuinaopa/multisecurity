﻿FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
 
COPY ./bin/Release/net6.0/publish /app
ENV ASPNETCORE_URLS=http://*:8080
ENV ASPNETCORE_ENVIRONMENT docker
 
EXPOSE 8080
 
ENTRYPOINT ["dotnet", "/app/multitrabajos-cuenta.dll"]