FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build
WORKDIR /App

EXPOSE 3306

# Copy everything
COPY . ./

# Запуск
#RUN dotnet test
#ENTRYPOINT ["tail", "-f", "/dev/null"]
ENTRYPOINT ["/bin/bash", "pet_shop/testLoop.sh"]
