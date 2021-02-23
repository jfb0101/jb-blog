namespace FsharpBackend.UseCases.User


open Cassandra
open System.Linq
open FsharpBackend.Utils.Security
open FsharpBackend.DB.RowToObj
open System

module Login =

    let md5Match (passwordInMD5:string) (informedPassword:string) = (GetStringMD5.``$`` informedPassword)  = passwordInMD5

    let ``$`` (session:ISession) (email:string) (password:string) = 
        let stm = session.Prepare("select id,name,email,password from user where email = ?").Bind(email)
        let rs = session.Execute(stm)

        let user = rs.First() |> UserRowToObj.``$``

        match user with
            | None -> None
            | Some(u) -> if (md5Match u.Password password) then Some(Guid.NewGuid().ToString()) else None
