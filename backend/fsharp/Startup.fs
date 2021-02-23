namespace fsharp

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.HttpsPolicy;
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Microsoft.AspNetCore.Http
open System.Reflection
open FsharpBackend.Web.Security
open System.Linq
open Microsoft.AspNetCore.Mvc.Controllers
open FsharpBackend.UseCases.User
open FsharpBackend.DB.Redis
open FsharpBackend
open FsharpBackend.Middleware


            
            


type Startup(configuration: IConfiguration) =
    member _.Configuration = configuration

    // This method gets called by the runtime. Use this method to add services to the container.
    member _.ConfigureServices(services: IServiceCollection) =
        // Add framework services.
        services.AddControllers() |> ignore

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    member _.Configure(app: IApplicationBuilder, env: IWebHostEnvironment) =
        if (env.IsDevelopment()) then
            app.UseDeveloperExceptionPage() |> ignore
        app.UseHttpsRedirection()
           .UseRouting()
           .UseAuthorization()
           .UseMiddleware<BlogAuthMiddleware>()
           .UseEndpoints(fun endpoints ->
                endpoints.MapControllers() |> ignore
            ) |> ignore
