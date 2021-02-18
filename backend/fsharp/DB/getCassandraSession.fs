namespace FsharpBackend.DB
open Cassandra

module Cassandra =
    let getCassandraSession () =
        let cluster = Cluster.Builder().AddContactPoint("localhost").Build()

        cluster.Connect("jb_blog")
