#!/bin/bash

#load product data to elasticsearch

while [ `curl -s -o /dev/null -w "%{http_code}" http://elk:9200` != 200 ]; do
    echo "waiting for enable elasticsearch"
    sleep 1
done

curl  -XPUT 'http://elk:9200/product?pretty' -H 'Content-Type: application/json' -d' {"settings" : {"number_of_shards" : 1,"number_of_replicas":0},"mappings" : {"product" : {"properties" : {"id" : { "type" : "keyword" },"name" : { "type" : "text" },"lastUpdateDate" : { "type" : "date" }}}},"aliases" : {"alias-product" : {}}}' -o /dev/null
dotnet Equinox.WebApi.dll