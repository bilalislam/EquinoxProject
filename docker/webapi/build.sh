#!/bin/bash

# Define image name and version
IMAGE_NAME=ninjafx/eq-webapi
VERSION=1.0


# run build script for dockerfile
projectList=(
    "../../src/Equinox.WebApi"
)

for project in "${projectList[@]}"
do
    echo -e "\e[33mWorking on $(pwd)/$project"
    echo -e "\e[33m\tRemoving old publish output"
    pushd $(pwd)/$project
    rm -rf obj/Docker/publish
    echo -e "\e[33m\tBuilding and publishing projects"
    dotnet restore
    dotnet build
    dotnet publish -o obj/Docker/publish -c Release
    popd
done

# remove old docker images & containers
docker rmi $(docker images --filter=reference="ninjafx/eq-webapi" -q) -f
docker rm $(docker ps --filter=ancestor="ninjafx/eq-webapi" -q) -f
docker rm $(docker ps --filter=ancestor="dockercompose_webapi_1" -q) -f

# Create and Copy latest built dll file into docker folder
mkdir app
mv ../../src/Equinox.WebApi/obj/Docker/publish app/
rm -rf ../../src/Equinox.WebApi/obj/Docker

# Build docker image
docker build -f Dockerfile -t "$IMAGE_NAME:$VERSION" .

# Tag this version as latest
docker tag "$IMAGE_NAME:$VERSION" "$IMAGE_NAME:latest"

# Push docker image as latest
docker push "$IMAGE_NAME:latest"

# Remove built jar file
rm -rf app