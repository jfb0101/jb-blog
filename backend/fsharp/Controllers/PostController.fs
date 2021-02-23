namespace FsharpBackend.Controllers

open Microsoft.AspNetCore.Mvc
open FsharpBackend.Models
open System

open FsharpBackend.UseCases.User
open FsharpBackend.UseCases.Post
open FsharpBackend.DB.Cassandra
open FsharpBackend.UseCases

open FsharpBackend

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

        match user with
            | Some(_user) -> CreatePost.``$`` session data.post _user
            | None -> Error()

        