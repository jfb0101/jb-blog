namespace FsharpBackend.UseCases.User
open Cassandra

open System.Linq;
open FsharpBackend.Models
open FsharpBackend.DB.RowToObj

module GetUser =
    let ``$`` (session:ISession) (userId:string) : User option  =
        let stm = session.Prepare("select id,name,email from user where id = ?").Bind(userId)
        let rs = session.Execute(stm)

        rs.First() |> UserRowToObj.``$``
            
            

