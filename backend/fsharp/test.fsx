#r "bin/Debug/net5.0/fsharp-backend.dll"

open FsharpBackend.UseCases.User

let redisClient = FsharpBackend.DB.Redis.getRedisClient()

