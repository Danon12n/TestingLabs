FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /App

EXPOSE 3306

# Copy everything
COPY . ./

# Запуск
#RUN dotnet test
#ENTRYPOINT ["tail", "-f", "/dev/null"]
ENTRYPOINT ["/bin/bash", "pet_shop.IT/testLoop.sh"]
