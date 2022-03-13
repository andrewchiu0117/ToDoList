





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

module ConfigurationCors =
   let ConfigureCors(corsBuilder: CorsPolicyBuilder): unit =        
         corsBuilder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials() |> ignore
open ConfigurationCors
open Microsoft.OpenApi.Models

type Startup private () =
    new (configuration: IConfiguration) as this =
        Startup() then
        this.Configuration <- configuration

    // This method gets called by the runtime. Use this method to add services to the container.
    member this.ConfigureServices(services: IServiceCollection) =
        // Add framework services.
        services.AddCors(fun options -> 
            //options.AddPolicy("AllowAll", fun builder -> 
            //     builder.AllowAnyHeader()
            //            .AllowAnyOrigin()
            //            .WithMethods("POST")
            //            .AllowCredentials() |> ignore))
            options.AddDefaultPolicy(fun builder->
                    builder.AllowAnyMethod() |>ignore))
            //.AddGiraffe()
            |> ignore
        services.AddControllers() |> ignore
        let info = OpenApiInfo()
        services.AddSwaggerGen(fun config -> config.SwaggerDoc("v1",info)) |> ignore
        //services.AddSwaggerGen(); // 註冊 Swagger
        //services.AddOpenApiDocument() |> ignore // 註冊服務加入 OpenAPI 文件
        services.AddHostedService()

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    member this.Configure(app: IApplicationBuilder, env: IWebHostEnvironment) =
        //app.UseOpenApi();    // 啟動 OpenAPI 文件
        //app.UseSwaggerUi3(); // 啟動 Swagger UI
        app.UseSwagger()
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