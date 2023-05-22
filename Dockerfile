    # executar comando abaixo para build do projeto em linux
	# dotnet publish -r linux-musl-x64 --output "..\linux64_musl" --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=true

    FROM mcr.microsoft.com/dotnet/runtime-deps:6.0-alpine-amd64

    EXPOSE 8080

    RUN mkdir /app
    WORKDIR /app
    COPY ./linux64_musl/. ./

    RUN chmod +x ./WebAppKey
    CMD ["./WebAppKey", "--urls", "http://0.0.0.0:8080"]