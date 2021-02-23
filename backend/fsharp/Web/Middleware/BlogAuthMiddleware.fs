namespace FsharpBackend.Middleware
open Microsoft.AspNetCore.Http
open System.Threading.Tasks
open System.Reflection
open System.Linq
open FsharpBackend.DB.Redis
open FsharpBackend.UseCases.User
open FsharpBackend
open Microsoft.AspNetCore.Mvc.Controllers
open FsharpBackend.Web.Security

type BlogAuthMiddleware(next: RequestDelegate) =
    member _.Invoke(context:HttpContext) : Task =
        
        let methodHasBlogAuthAttribute (methodInfo: MethodInfo) =
            not (isNull (methodInfo.GetCustomAttributes().FirstOrDefault(fun attr -> attr :? BlogAuth)))

        let tokenIsValid token =
            
            let redisClient = getRedisClient()
            match GetTokenStatus.``$`` redisClient token with
                | Success(GetTokenStatus.Valid) -> true
                | _ -> false
        
        let controllerActionDescriptor = Option.ofObj(context.GetEndpoint())
                                            |> Option.map(fun e -> e.Metadata)
                                            |> Option.map(fun m -> m.GetMetadata<ControllerActionDescriptor>())

        let proceed =   if controllerActionDescriptor.IsNone then true
                        elif methodHasBlogAuthAttribute controllerActionDescriptor.Value.MethodInfo then
                            tokenIsValid (context.Request.Headers.["Authorization"].ToString())
                        else true     

        if proceed then next.Invoke(context) 
        else
            printfn "nao continuar"
            context.Response.StatusCode <- 403
            context.Response.WriteAsync("não autorizado")