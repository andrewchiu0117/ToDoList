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
        let DBresult = ilistService.DeleteListBy(id)
        let result = DBresult.Response[0].ToString()
        ActionResult<string>(result)

    [<HttpDelete>]
    [<Route("deleteCompleted")>]
    member this.DeleteCompleted() = 
        let DBresult = ilistService.DeleteListCompleted()
        let result = DBresult.Response[0].ToString()
        ActionResult<string>(result)
    
    [<HttpGet>]
    [<Route("lists")>]
    member this.GetAll() = 
        let a= ilistService.GetAllList()
        ActionResult<Object>(a)

    [<HttpPut>]
    member this.Update([<FromBody>] item : UpdateList) = 
        let DBresult = ilistService.UpdateList(item)
        let result = DBresult.Response[0].ToString()
        ActionResult<string>(result)

    [<HttpPut>]
    [<Route("checkAll")>]
    member this.UpdateCheckAll([<FromBody>] checks : CheckAll) = 
        let result = ilistService.UpdateListCheckAll(checks)
        ActionResult<bool>(true)

    


