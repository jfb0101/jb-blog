namespace FsharpBackend.Controllers

open Microsoft.AspNetCore.Mvc
open FsharpBackend.Models
open System

open FsharpBackend.UseCases.User
open FsharpBackend.UseCases.Post
open FsharpBackend.DB.Cassandra
open FsharpBackend.UseCases
open FsharpBackend.Web.Security

open FsharpBackend
open System.Threading.Tasks

type CreateContract =
    {userId:string; post: Post}


[<ApiController>]
[<Route("[controller]")>]
type PostsController () =
    inherit ControllerBase()

    [<HttpPost>]
    [<BlogAuth>]
    member this.Create ([<FromBody>] data:CreateContract) : IActionResult  =
        
        use session = getCassandraSession()

        let user = GetUser.``$`` session data.userId

        if user.IsNone then this.NotFound("User not found") :> IActionResult
        else
            match CreatePost.``$`` session data.post user.Value with
                | Success(post) -> this.Ok(post) :> IActionResult
                | Error(e) -> this.BadRequest(e) :> IActionResult


        

        