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
open FsharpBackend.DB.Redis
open FsharpBackend

[<ApiController>]
[<Route("[controller]")>]
type UserController () = 
    inherit ControllerBase()

    [<HttpPost>]
    member _.Create([<FromBody>] user: User) =
        let session = getCassandraSession()
        let result = CreateUser.``$`` session user

        match result with
            | Success() -> $"usuario criado"
            | Error() -> $"falha ao criar usuario"

    [<HttpPost>]
    [<Route("Login")>]
    member _.LoginAction([<FromForm>] email:string, [<FromForm>] password:string) =
        Console.WriteLine($"email: {email}, password: {password}")

        let session = getCassandraSession()
        let redisClient = getRedisClient()

        match Login.``$`` session redisClient email password with
            | Error(Login.EmailNotFound) -> "Email não encontrado"
            | Error(Login.WrongPassword) -> "Senha inválida"
            | Error(Login.NotSavedOnRedis) -> "Falha ao persistir no redis"
            | Success(token) -> token
            
    [<HttpGet>]
    [<Route("ValidateToken")>]
    member _.ValidateToken(token:string) = 
        let redisClient = getRedisClient()

        match GetTokenStatus.``$`` redisClient token with
            | Success(GetTokenStatus.Valid) -> "token válido"
            | Success(GetTokenStatus.NotFound) -> "token não encontrado"
            | _ -> "erro"
        