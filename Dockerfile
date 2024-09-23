# 使用官方 .NET SDK 镜像作为构建环境
FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app
# 复制 csproj 并还原依赖
COPY . ./ 

RUN chown -R app /app 
# 生成运行时镜像
RUN dotnet restore

ENV ASPNETCORE_ENVIRONMENT=Production
# 暴露端口
EXPOSE 5176 
# 启动应用
ENTRYPOINT ["dotnet", "run", "--urls=http://0.0.0.0:5176"]
