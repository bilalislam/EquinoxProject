#!/bin/bash

# Swarm mode using Docker Machine

managers=1
workers=3

# create manager machines
echo "======> Leave swarm mode  $managers manager machines ...";
for node in $(seq 1 $managers);
do
	echo "======> stop swarm-$node machine ...";
	docker-machine ssh swarm-$node  "docker swarm leave -f";
	docker-machine ssh swarm-$node  "docker stack rm ninjafxlabs";
	docker-machine ssh swarm-$node  "docker network rm ninjafxlabs_webnet";
    #docker-machine stop swarm-$node
done

# create worker machines
echo "======> Leave swarm mode  $workers worker machines ...";
for node in $(seq 2 $workers);
do
	echo "======> stop swarm-$node machine ...";
	docker-machine ssh swarm-$node  "docker swarm leave -f";
    #docker-machine stop swarm-$node
done
