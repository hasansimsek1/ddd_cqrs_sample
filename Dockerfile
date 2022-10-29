FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
RUN mkdir -p /basketapp/src
RUN mkdir -p /basketapp/dist
COPY . /basketapp/src
WORKDIR /basketapp/src
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /basketapp/dist
COPY --from=build-env /basketapp/src/out .
EXPOSE 80
CMD [ "dotnet", "Presentation.Api.dll"]