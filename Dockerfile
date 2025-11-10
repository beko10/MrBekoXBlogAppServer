# 1. ADIM: Build için SDK image'i kullan
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# 2. ADIM: Proje dosyalarını kopyala
COPY ["MrBekoXBlogAppServer.sln", "./"]
COPY ["Presentation/MrBekoXBlogAppServer.API/MrBekoXBlogAppServer.API.csproj", "Presentation/MrBekoXBlogAppServer.API/"]
COPY ["Core/MrBekoXBlogAppServer.Application/MrBekoXBlogAppServer.Application.csproj", "Core/MrBekoXBlogAppServer.Application/"]
COPY ["Core/MrBekoXBlogAppServer.Domain/MrBekoXBlogAppServer.Domain.csproj", "Core/MrBekoXBlogAppServer.Domain/"]
COPY ["Infrastructure/MrBekoXBlogAppServer.Infrastructure/MrBekoXBlogAppServer.Infrastructure.csproj", "Infrastructure/MrBekoXBlogAppServer.Infrastructure/"]
COPY ["Infrastructure/MrBekoXBlogAppServer.Persistence/MrBekoXBlogAppServer.Persistence.csproj", "Infrastructure/MrBekoXBlogAppServer.Persistence/"]

# 3. ADIM: Paketleri yükle
RUN dotnet restore "MrBekoXBlogAppServer.sln"

# 4. ADIM: Tüm dosyaları kopyala ve build et
COPY . .
WORKDIR "/src/Presentation/MrBekoXBlogAppServer.API"
RUN dotnet build "MrBekoXBlogAppServer.API.csproj" -c Release -o /app/build

# 5. ADIM: Publish (yayınla)
FROM build AS publish
RUN dotnet publish "MrBekoXBlogAppServer.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 6. ADIM: Runtime image (çalışma zamanı)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# 7. ADIM: Port'ları aç (8080 ve 8081)
EXPOSE 8080
EXPOSE 8081

# 8. ADIM: Publish edilmiş dosyaları kopyala
COPY --from=publish /app/publish .

# 9. ADIM: Uygulamayı başlat
ENTRYPOINT ["dotnet", "MrBekoXBlogAppServer.API.dll"]