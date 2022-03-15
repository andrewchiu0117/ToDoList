namespace TodoList
#nowarn "20"
open System
open System.Collections.Generic
open System.IO
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.HttpsPolicy
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Startup

[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
module Program =
    let exitCode = 0
    let CreateHostBuilder args =
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(fun webBuilder ->
                webBuilder.UseStartup<Startup>() |> ignore )

    [<EntryPoint>]
    let main args =
        

        let builder = WebApplication.CreateBuilder(args)

        builder.Services.AddControllers()
        builder.Services.AddSwaggerGen()

        let app = builder.Build()

        app.UseHttpsRedirection()
        app.UseCors(fun options ->
            options.AllowAnyMethod()
            options.AllowAnyOrigin()
            options.AllowAnyHeader()
            |> ignore);

        app.UseAuthorization()
        app.MapControllers()
        app.UseSwagger()
        app.UseSwaggerUI()

        app.Run()

        exitCode
