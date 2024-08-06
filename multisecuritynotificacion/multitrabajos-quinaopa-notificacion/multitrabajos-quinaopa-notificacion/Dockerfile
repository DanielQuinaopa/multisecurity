FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
 
COPY ./publish /app
ENV ASPNETCORE_URLS=http://*:8083
ENV ASPNETCORE_ENVIRONMENT Docker
 
EXPOSE 8083
 
ENTRYPOINT ["dotnet", "/app/multitrabajos-quinaopa-notificacion.dll"]