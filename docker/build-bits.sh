#!/bin/sh

# List of microservices here needs to be updated to include all the new microservices (Marketing, etc.)

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
    dotnet publish -o obj/Docker/publish -c Release
    popd
done

# remove old docker images:
images=$(docker images --filter=reference="ninjafx/*" -q)
if [ -n "$images" ]; then
    docker rm $(docker ps -a -q) -f
    echo "Deleting ninjafx images in local Docker repo"
    echo $images
    docker rmi $(docker images --filter=reference="ninjafx/*" -q) -f
fi

# No need to build the images, docker build or docker compose will
# do that using the images and containers defined in the docker-compose.yml file.