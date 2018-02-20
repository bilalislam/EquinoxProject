#!/bin/bash

# Define image name and version
IMAGE_NAME=ninjafx/eq-webapi
VERSION=1.0


# run build script for dockerfile
sh ../../cli-mac/build-bits.sh

# Create and Copy latest built dll file into docker folder
mkdir app
mv ../../src/Equinox.WebApi/obj/Docker/publish app/
rm -rf ../../src/Equinox.WebApi/obj/Docker

# Build docker image
docker build -f Dockerfile -t "$IMAGE_NAME:$VERSION" .

# Tag this version as latest
docker tag "$IMAGE_NAME:$VERSION" "$IMAGE_NAME:latest"

# Remove built jar file
rm -rf app