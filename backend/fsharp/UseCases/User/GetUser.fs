namespace FsharpBackend.UseCases.User
open Cassandra

open System.Linq;
open FsharpBackend.Models

module GetUser =
    let ``$`` (session:ISession) (userId:string) : User  =
        let stm = session.Prepare("select id,name from user where id = ?").Bind(userId)
        let rs = session.Execute(stm)

        let user = rs.First() |> (fun row -> {
            Id = row.GetValue("id");
            Name = row.GetValue("name");
            Password = null
        })

        user
