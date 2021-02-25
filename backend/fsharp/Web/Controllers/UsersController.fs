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
open FsharpBackend.Web.Security

[<ApiController>]
[<Route("[controller]")>]
type UsersController () = 
    inherit ControllerBase()

    [<HttpPost>]
    member this.Create([<FromBody>] user: User) : IActionResult =
        use session = getCassandraSession()
        let result = CreateUser.``$`` session user

        match result with
            | Success(user) -> this.Ok(user) :> IActionResult
            | Error(e) -> this.Problem(e) :> IActionResult

    [<HttpPost>]
    [<Route("Login")>]
    member this.LoginAction([<FromForm>] email:string, [<FromForm>] password:string) : IActionResult =

        use session = getCassandraSession()
        use redisClient = getRedisClient()

        match Login.``$`` session redisClient email password with
            | Error(Login.EmailNotFound) | Error(Login.WrongPassword) -> this.BadRequest("Email or password wrong") :> IActionResult
            | Error(Login.ErrorPersistingToken) -> this.Problem("System error") :> IActionResult
            | Success(token) -> this.Ok(token) :> IActionResult
            
    [<HttpGet>]
    [<Route("ValidateToken")>]
    
    member this.ValidateToken(token:string) = 
        use redisClient = getRedisClient()

        match GetTokenStatus.``$`` redisClient token with
            | Success(GetTokenStatus.Valid) -> this.Ok() :> IActionResult
            | Success(GetTokenStatus.NotFound) -> this.BadRequest() :> IActionResult
            | Error(e)  -> this.Problem(e) :> IActionResult
            | _ -> this.Problem() :> IActionResult
        