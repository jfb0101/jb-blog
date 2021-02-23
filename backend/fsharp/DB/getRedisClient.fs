namespace FsharpBackend.DB
open ServiceStack.Redis

module Redis = 
    let getRedisClient () = 
        let redisManager = new RedisManagerPool("localhost:6379")
        let redisClient = redisManager.GetClient()

        redisClient