namespace ToDoList

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
        ActionResult<Object>(listService.Create(_ToDoItem))

    [<HttpDelete>]
    [<Route("{id}")>]
    member this.Delete(id :string) = 
        listService.DeleteListBy(id) |> ignore
        ActionResult<bool>(true)

    [<HttpDelete>]
    [<Route("deleteCompleted")>]
    member this.DeleteCompleted() = 
        listService.DeleteListCompleted() |> ignore
        ActionResult<bool>(true)
    
    [<HttpGet>]
    [<Route("lists")>]
    member this.GetAll() = 
        let a= listService.GetAllList()
        ActionResult<Object>(a)

    [<HttpPut>]
    member this.Update([<FromBody>] item : UpdateList) = 
        listService.UpdateList(item) |> ignore
        ActionResult<bool>(true)

    [<HttpPut>]
    [<Route("checkAll")>]
    member this.UpdateCheckAll([<FromBody>] checks : CheckAll) = 
        listService.UpdateListCheckAll(checks) |> ignore
        ActionResult<bool>(true)

    


