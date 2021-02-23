namespace fsharp.Controllers

open Microsoft.AspNetCore.Mvc
open Cassandra
open System.Linq;

open FsharpBackend.Models
open FsharpBackend.DB.Cassandra
open FsharpBackend.UseCases.User
open FsharpBackend.UseCases
open FsharpBackend.UseCases.User
open System

[<ApiController>]
[<Route("[controller]")>]
type UserController () = 
    inherit ControllerBase()

    [<HttpPost>]
    member _.Create([<FromBody>] user: User) =
        let session = getCassandraSession()
        let result = CreateUser.``$`` session user

        match result with
            | (Success) -> $"usuario criado"
            | (Error) -> $"falha ao criar usuario"

    [<HttpPost>]
    [<Route("Login")>]
    member _.LoginAction([<FromForm>] email:string, [<FromForm>] password:string) =
        Console.WriteLine($"email: {email}, password: {password}")
        let session = getCassandraSession()

        match Login.``$`` session email password with
            | (Error) -> "usuario ou senha invalidos"
            | (Success) -> "sucesso"

        