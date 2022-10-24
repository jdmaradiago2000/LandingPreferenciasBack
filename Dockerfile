FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app

COPY Cobros_Consola_Landing_Preferencias .

ENTRYPOINT ["dotnet", "COBROS_WebApiLandingPreferencias.dll"]