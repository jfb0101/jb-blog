namespace FsharpBackend.UseCases.User

open FsharpBackend
open ServiceStack.Redis

module GetTokenStatus = 
    type TokenStatus =
        | Valid
        | Expired
        | NotFound

    let ``$`` (redisClient:IRedisClient) token : Result<TokenStatus,_> =
        let email = redisClient.Get<string>(token)

        match email with
            | null -> Success(NotFound)
            | _ -> Success(Valid)
