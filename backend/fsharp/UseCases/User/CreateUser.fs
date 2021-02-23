namespace FsharpBackend.UseCases.User
open Cassandra
open FsharpBackend.Models
open System
open System.Security.Cryptography
open System.Text
open FsharpBackend.UseCases
open FsharpBackend.Utils.Security
open FsharpBackend

module CreateUser = 

    let ``$`` (session:ISession) (user:User) : Result<_,_> = 
        let id = Guid.NewGuid().ToString()
        let stm = session.Prepare("insert into user (id, name,email,password) values (?, ?,?,?)").Bind(id,user.Name, user.Email, GetStringMD5.``$`` user.Password)

        session.Execute(stm) |> ignore

        Success()
    

        