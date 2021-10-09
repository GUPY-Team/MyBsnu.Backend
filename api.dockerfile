FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /sln

COPY *.sln .

COPY src/Host/Bootstrapper/Bootstrapper.csproj ./src/Host/Bootstrapper/Bootstrapper.csproj

COPY src/Modules/Identity/Modules.Identity/Modules.Identity.csproj ./src/Modules/Identity/Modules.Identity/Modules.Identity.csproj
COPY src/Modules/Identity/Modules.Identity.Core/Modules.Identity.Core.csproj ./src/Modules/Identity/Modules.Identity.Core/Modules.Identity.Core.csproj
COPY src/Modules/Identity/Modules.Identity.Infrastructure/Modules.Identity.Infrastructure.csproj ./src/Modules/Identity/Modules.Identity.Infrastructure/Modules.Identity.Infrastructure.csproj

COPY src/Modules/Timetable/Modules.Timetable/Modules.Timetable.csproj ./src/Modules/Timetable/Modules.Timetable/Modules.Timetable.csproj
COPY src/Modules/Timetable/Modules.Timetable.Core/Modules.Timetable.Core.csproj ./src/Modules/Timetable/Modules.Timetable.Core/Modules.Timetable.Core.csproj
COPY src/Modules/Timetable/Modules.Timetable.Infrastructure/Modules.Timetable.Infrastructure.csproj ./src/Modules/Timetable/Modules.Timetable.Infrastructure/Modules.Timetable.Infrastructure.csproj

COPY src/Shared/Shared.Core/Shared.Core.csproj ./src/Shared/Shared.Core/Shared.Core.csproj
COPY src/Shared/Shared.DTO/Shared.DTO.csproj ./src/Shared/Shared.DTO/Shared.DTO.csproj
COPY src/Shared/Shared.Infrastructure/Shared.Infrastructure.csproj ./src/Shared/Shared.Infrastructure/Shared.Infrastructure.csproj
COPY src/Shared/Shared.Localization/Shared.Localization.csproj ./src/Shared/Shared.Localization/Shared.Localization.csproj

RUN dotnet restore

COPY src/. ./src
RUN dotnet build -c Release --no-restore

RUN dotnet publish -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app

COPY --from=build /app ./

ENV ASPNETCORE_URLS=http://+:5000

ENTRYPOINT ["dotnet", "Bootstrapper.dll"]
