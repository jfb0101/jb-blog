namespace fsharp.Controllers

open Microsoft.AspNetCore.Mvc
open Cassandra
open System.Linq;

open FsharpBackend.Models
open FsharpBackend.DB.Cassandra
open FsharpBackend.UseCases.User
open FsharpBackend.UseCases

[<ApiController>]
[<Route("[controller]")>]
type UserController () = 
    inherit ControllerBase()

    [<HttpPost>]
    member _.Create([<FromBody>] user: User) =
        let session = getCassandraSession()
        let result = CreateUser.``$`` session user

        match result with
            | (Success, _) -> $"usuario criado"
            | (Error, erro) -> $"falha ao criar usuario"

        