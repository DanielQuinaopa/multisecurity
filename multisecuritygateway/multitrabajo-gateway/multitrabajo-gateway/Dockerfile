﻿FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
 
COPY ./publish /app
ENV ASPNETCORE_URLS=http://*:8085
ENV ASPNETCORE_ENVIRONMENT docker
 
EXPOSE 8085
 
ENTRYPOINT ["dotnet", "/app/multitrabajo-gateway.dll"]