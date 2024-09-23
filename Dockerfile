# 使用官方 .NET SDK 镜像作为基础镜像
FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app

# 安装 dotnet-watch 工具
RUN dotnet tool install --global dotnet-watch

# 设置环境变量，以确保工具可用
ENV PATH="${PATH}:/root/.dotnet/tools"

# 设置环境变量
ENV ASPNETCORE_URLS=http://+:5176
ENV DOTNET_USE_POLLING_FILE_WATCHER=1

# 暴露端口
EXPOSE 5176

# 启动应用
ENTRYPOINT ["dotnet", "watch", "run", "--urls=http://0.0.0.0:5176"]
