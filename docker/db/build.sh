#!/bin/bash

IMAGE_NAME=ninjafx/eq-database
VERSION=1.0

# Build docker image
docker build -f Dockerfile -t "$IMAGE_NAME:$VERSION" .

# Tag this version as latest
docker tag "$IMAGE_NAME:$VERSION" "$IMAGE_NAME:latest"

# Push docker image as latest
docker push "$IMAGE_NAME:latest"