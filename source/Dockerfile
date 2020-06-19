FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /source

COPY . .
# COPY O2.Services.Certificates.sln O2.Services.Certificates.sln
# COPY O2.Services.Certificates.API O2.Services.Certificates.API
# COPY O2.Services.Certificates.Business.Impl O2.Services.Certificates.Business.Impl
# COPY O2.Services.Certificates.Business  O2.Services.Certificates.Business
# COPY O2.Services.Certificates.Data O2.Services.Certificates.Data


COPY . .

RUN dotnet restore "/source/O2.Services.Certificates.sln"


WORKDIR /source/O2.Services.Certificates.API
RUN dotnet publish --no-restore -c Release -o /app

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "O2.Services.Certificates.API.dll"]