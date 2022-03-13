namespace ToDoList

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open IService
open Service
open ContractModel
open Entity
open Repository

[<ApiController>]
[<Route("api/[controller]")>]
type ListController ()=
    inherit ControllerBase()
    static let ilistService = ListService(new ListRepository()) :> IListService<ListRepository>

    [<HttpGet>]
    [<Route("{id}")>]
    member this.Get(id:string) =
        ActionResult<Entity.ToDoListEntity>(ilistService.getListBy(id))

    [<HttpPost>]
    member this.Post([<FromBody>] _ToDoItem :CreateList) = 
        ActionResult<Object>(ilistService.Create(_ToDoItem))

    [<HttpDelete>]
    [<Route("{id}")>]
    member this.Delete(id :string) = 
        ilistService.DeleteListBy(id) |> ignore
        ActionResult<bool>(true)

    [<HttpDelete>]
    [<Route("deleteCompleted")>]
    member this.DeleteCompleted() = 
        ilistService.DeleteListCompleted() |> ignore
        ActionResult<bool>(true)
    
    [<HttpGet>]
    [<Route("lists")>]
    member this.GetAll() = 
        let a= ilistService.GetAllList()
        ActionResult<Object>(a)

    [<HttpPut>]
    member this.Update([<FromBody>] item : UpdateList) = 
        ilistService.UpdateList(item) |> ignore
        ActionResult<bool>(true)

    [<HttpPut>]
    [<Route("checkAll")>]
    member this.UpdateCheckAll([<FromBody>] checks : CheckAll) = 
        ilistService.UpdateListCheckAll(checks) |> ignore
        ActionResult<bool>(true)

    


