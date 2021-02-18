namespace FsharpBackend.Controllers

open Microsoft.AspNetCore.Mvc
open FsharpBackend.Models
open System

open FsharpBackend.UseCases.User
open FsharpBackend.UseCases.Post
open FsharpBackend.DB.Cassandra

type CreateContract =
    {userId:string; post: Post}


[<ApiController>]
[<Route("[controller]")>]
type PostsController () =
    inherit ControllerBase()

    [<HttpPost>]
    member _.Create ([<FromBody>] data:CreateContract) =
        
        let session = getCassandraSession()

        let user = GetUser.``$`` session data.userId

        CreatePost.``$`` session data.post user