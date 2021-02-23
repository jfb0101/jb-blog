#r "bin/Debug/net5.0/fsharp-backend.dll"

open FsharpBackend.UseCases.User

let md5 = CreateUser.getMd5 "1234"

printfn $"{md5}"