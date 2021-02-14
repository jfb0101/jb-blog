#!/bin/bash

container_name=jb-blog-cassandra-db
queries_file=cassandra-queries.cql

docker cp cassandra-queries.cql $container_name:/tmp/$queries_file

docker exec -it jb-blog-cassandra-db cqlsh -f /tmp/$queries_file