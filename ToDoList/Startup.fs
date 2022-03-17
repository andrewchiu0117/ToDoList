




[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
module Startup

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.AspNetCore.Cors.Infrastructure
open Swashbuckle.AspNetCore.Swagger


[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
module ConfigurationCors =
   let ConfigureCors(corsBuilder: CorsPolicyBuilder): unit =        
         corsBuilder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials() |> ignore
open ConfigurationCors
open Microsoft.OpenApi.Models


[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
type Startup private () =
    new (configuration: IConfiguration) as this =
        Startup() then
        this.Configuration <- configuration

    // This method gets called by the runtime. Use this method to add services to the container.
    member this.ConfigureServices(services: IServiceCollection) =
        // Add framework services.
        services.AddCors(fun options -> 
            options.AddDefaultPolicy(fun builder->
                    builder.AllowAnyMethod() |>ignore))
            |> ignore
        services.AddControllers() |> ignore
        let info = OpenApiInfo()
        services.AddSwaggerGen(fun config -> config.SwaggerDoc("v1",info)) |> ignore
        services.AddHostedService()

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    member this.Configure(app: IApplicationBuilder, env: IWebHostEnvironment) =
        app.UseSwagger() |> ignore
        app.UseSwaggerUI(fun config -> config.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1")) |> ignore
        app.UseCors() |> ignore
        app.UseDeveloperExceptionPage()  |> ignore
        app.UseHttpsRedirection() |> ignore
        app.UseRouting() |> ignore

        app.UseAuthorization() |> ignore

        app.UseEndpoints(fun endpoints ->
            endpoints.MapControllers() |> ignore
            ) |> ignore

    member val Configuration : IConfiguration = null with get, set
