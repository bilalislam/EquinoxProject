#!/bin/bash

# Swarm mode using Docker Machine

managers=1
workers=3

# create manager machines
echo "======> Creating $managers manager machines ...";
for node in $(seq 1 $managers);
do
	echo "======> Creating swarm-$node machine ...";
	docker-machine create -d virtualbox swarm-$node;
	docker-machine start swarm-$node;
done

# create worker machines
echo "======> Creating $workers worker machines ...";
for node in $(seq 1 $workers);
do
	echo "======> Creating swarm-$node machine ...";
	docker-machine create -d virtualbox swarm-$node;
	docker-machine start swarm-$node;
done

# list all machines
docker-machine ls

# initialize swarm mode and create a manager
echo "======> Initializing first swarm manager ..."
docker-machine ssh swarm-1 "docker swarm init --listen-addr $(docker-machine ip swarm-1) --advertise-addr $(docker-machine ip swarm-1)"

# get manager and worker tokens
export worker_token=`docker-machine ssh swarm-1 "docker swarm join-token worker -q"`

echo "worker_token: $worker_token"

# show members of swarm
docker-machine ssh swarm-1 "docker node ls"

# workers join swarm
for node in $(seq 2 $workers);
do
	echo "======> swarm-$node joining swarm as worker ..."
	docker-machine ssh swarm-$node \
	"docker swarm join \
	--token $worker_token \
	--listen-addr $(docker-machine ip swarm-$node) \
	--advertise-addr $(docker-machine ip swarm-$node) \
	$(docker-machine ip swarm-1)"
done

# show members of swarm
#docker-machine ssh swarm-1 "docker node ls"
eval $(docker-machine env swarm-1) \ 
docker stack deploy -c docker-compose.yml ninjafxlabs
