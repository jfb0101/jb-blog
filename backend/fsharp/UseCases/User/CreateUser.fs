namespace FsharpBackend.UseCases.User
open Cassandra
open FsharpBackend.Models
open System
open System.Security.Cryptography
open System.Text
open FsharpBackend.UseCases

module CreateUser = 
    let getMd5 (str:string) = 
            let md5 = MD5.Create()

            let strBytes = UTF8Encoding().GetBytes(str)
            let hashBytes = md5.ComputeHash(strBytes)

            hashBytes
            |> Array.map (fun b -> System.String.Format("{0:X2}",b))
            |> String.concat System.String.Empty


    let ``$`` (session:ISession) (user:User) = 
        let id = Guid.NewGuid().ToString()
        let stm = session.Prepare("insert into user (id, name,password) values (?, ? ,?)").Bind(id,user.Name, getMd5 user.Password)

        session.Execute(stm) |> ignore

        (Success, null)
    

        