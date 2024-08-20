FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
 
COPY ./publish /app
ENV ASPNETCORE_URLS=http://*:8079
ENV ASPNETCORE_ENVIRONMENT docker
 
EXPOSE 8079
 
ENTRYPOINT ["dotnet", "/app/multisecurity.dll"]