namespace FsharpBackend.UseCases.Post
open Cassandra
open FsharpBackend.Models
open FsharpBackend.UseCases
open FsharpBackend

open System

module CreatePost =
    let ``$`` (session:ISession) (post:Post) (user:User) : Result<Post,_> = 
        let id = Guid.NewGuid().ToString()

        let stm = session.Prepare("insert into post (id, title, body, user_id , user_name , created_on) values (?,?,?,?,?,?)").
                    Bind(
                        id,
                        post.Title,
                        post.Body,
                        user.Id,
                        user.Name,
                        DateTime.Now)
        session.Execute(stm) |> ignore

        Success({post with Id = id})