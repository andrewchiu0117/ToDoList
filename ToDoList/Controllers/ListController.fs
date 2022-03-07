﻿namespace ToDoList

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open Service
open ContractModel
open Entity
open Repository

[<ApiController>]
[<Route("api/[controller]")>]
type ListController ()=
    inherit ControllerBase()
     static let listService = new ListService(new ListRepository())
    [<HttpGet>]
    [<Route("{id}")>]
    member this.Get(id:string) = 
        ActionResult<Entity.ToDoListEntity>(listService.getListBy(id))

    [<HttpPost>]
    member this.Post([<FromBody>] _ToDoItem :CreateList) = 
        ActionResult<string>(listService.Create(_ToDoItem))

    [<HttpDelete>]
     [<Route("{id}")>]
    member this.Delete(id :string) = 
        listService.DeleteListBy(id)
        ActionResult<bool>(true)
    
    [<HttpGet>]
    [<Route("lists")>]
    member this.GetAll() = 
        let a= listService.GetAllList()
        ActionResult<Object>(a)


    [<HttpPut>]
    member this.Update([<FromBody>] item : UpdateList) = 
        listService.UpdateList(item)
        ActionResult<bool>(true)

    


