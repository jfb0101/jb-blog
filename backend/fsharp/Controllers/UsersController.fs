namespace fsharp.Controllers

open Microsoft.AspNetCore.Mvc
open Cassandra
open System.Linq;

type User = 
    { LastName: string}

[<ApiController>]
[<Route("[controller]")>]
type UsersController () = 
    inherit ControllerBase()

    [<HttpGet>]
    member _.Get() =
        let cluster = Cluster.Builder().AddContactPoint("localhost").Build()

        let session = cluster.Connect("my_keyspace")

        let rs = session.Execute("select * from user")

        rs.Select(fun row -> 
            {LastName = row.GetValue<string>("last_name")})
