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
    member this.Get() = 
        let values = [| "values";"values"|]
        ActionResult<string[]>(values)

    [<HttpPost>]
    member this.Post([<FromBody>] _ToDoItem :CreateList) = 
        let a= listService.Create(_ToDoItem)
        ActionResult<string>(a)

    [<HttpDelete("{id}")>]
    member this.Delete(id :string) = 
        ActionResult<string>(id)
    
    [<HttpGet>]
    [<Route("lists")>]
    member this.GetAll() = 
        ActionResult<ToDoListEntity list>(listService.GetAllList())


    [<HttpPut>]
    [<Route("lists")>]
    member this.Update([<FromBody>] item : UpdateList) = 
       // listService.UpdateList(item)
        ActionResult<bool>(true)

    


