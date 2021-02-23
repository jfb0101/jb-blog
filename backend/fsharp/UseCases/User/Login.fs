namespace FsharpBackend.UseCases.User


open Cassandra
open System.Linq
open FsharpBackend.Utils.Security
open FsharpBackend.DB.RowToObj
open System
open FsharpBackend.DB.Redis
open FsharpBackend.UseCases

module Login =

    let md5Match (passwordInMD5:string) (informedPassword:string) = (GetStringMD5.``$`` informedPassword)  = passwordInMD5

    let saveTokenInRedis token email = 
        let redis = getRedisClient()
        redis.Set(token,email) |> ignore

        (Success)

    let ``$`` (session:ISession) (email:string) (password:string) = 
        let stm = session.Prepare("select id,name,email,password from user where email = ?").Bind(email)
        let rs = session.Execute(stm)

        let user' = rs.First() |> UserRowToObj.``$``

        let token' = match user' with
                        | None -> None
                        | Some(user) -> if (md5Match user.Password password) then Some(Guid.NewGuid().ToString()) else None

        Console.WriteLine("token: " + if token'.IsSome then token'.Value else "sem token")


        let result = match token' with
                        | Some(token) -> saveTokenInRedis token user'.Value.Email
                        | None -> (Error)

        result