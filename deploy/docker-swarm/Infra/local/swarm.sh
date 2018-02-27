#!/bin/bash

#setup docker swarm mode
docker swarm init

#install visualizer
docker service create \
  --name=viz \
  --publish=8080:8080/tcp \
  --constraint=node.role==manager \
  --mount=type=bind,src=/var/run/docker.sock,dst=/var/run/docker.sock \
  dockersamples/visualizer

#install web api
docker service create \
    --name=eq-web-api \
    --publish=8003:8003/tcp \
    --constraint=node.role==manager \
    --mount=type=bind,src=/var/run/docker.sock,dst=/var/run/docker.sock \
    ninjafx/eq-webapi
    
#install web 
docker service create \
    --name=eq-web \
    --publish=8002:8002/tcp \
    --constraint=node.role==manager \
    --mount=type=bind,src=/var/run/docker.sock,dst=/var/run/docker.sock \
    ninjafx/eq-web

#install db
docker service create \
     --name=db \
     --publish 1433:1433 \
     ninjafx/eq-database
