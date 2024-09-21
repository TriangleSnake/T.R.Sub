# 使用官方 .NET SDK 镜像作为构建环境
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# 复制 csproj 并还原依赖
COPY *.csproj ./
RUN dotnet restore

# 复制所有文件并构建
COPY . ./ 
RUN dotnet publish -c Release -o out

# 生成运行时镜像
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# 设置环境变量
ENV ASPNETCORE_URLS=http://+:5001
ENV ASPNETCORE_ENVIRONMENT=Production

# 暴露端口
EXPOSE 5001

# 启动应用
ENTRYPOINT ["dotnet", "T.R.Sub.dll"]
