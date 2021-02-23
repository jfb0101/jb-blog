namespace FsharpBackend.UseCases.User


open Cassandra
open System.Linq
open FsharpBackend.Utils.Security
open FsharpBackend.DB.RowToObj
open System
open FsharpBackend.DB.Redis
open FsharpBackend.UseCases
open FsharpBackend
open ServiceStack.Redis

module Login =

    type LoginError = 
        | EmailNotFound
        | WrongPassword
        | NotSavedOnRedis

    let md5Match (passwordInMD5:string) (informedPassword:string) = (GetStringMD5.``$`` informedPassword)  = passwordInMD5

    let saveTokenInRedis (redisClient:IRedisClient) token email = 

        let saveToken token = 

            match redisClient.Set(token,email) with
                | true -> Success()
                | false -> Error(NotSavedOnRedis)


        saveToken token
            


    let ``$`` (session:ISession) (redisClient:IRedisClient) (email:string) (password:string) : Result<string,LoginError> = 
        let stm = session.Prepare("select id,name,email,password from user where email = ?").Bind(email)
        let rs = session.Execute(stm)

        let user' = rs.First() |> UserRowToObj.``$``

        if user'.IsNone then Error(EmailNotFound)
        
        else 
            if not (md5Match user'.Value.Password password) then Error(WrongPassword)
            
            else
                let token = Guid.NewGuid().ToString()
                
                match saveTokenInRedis redisClient token email with
                    | Error(e) -> Error(e)
                    | Success() -> Success(token)
            