namespace ToDoList

open ContractModel
open Microsoft.FSharp.Linq
open Entity
open IRepository
open Repository
open System
open IService


module Service =
    type ListService(ilistRepository : IListRepository )=
        interface IListService<IListRepository> with 
            member this.ToListDetail(list: DBType.ToDoListModel) =
                let unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                {
                    Id = list._id.ToString();
                    Title=list.Title;
                    Priority=list.Priority;
                    Completed=list.Done;
                }
            
            member this.Create(list: CreateList)  = 
                let a = new  ToDoListEntity("",list.Title)
                let b = fun (list: ToDoListEntity) -> { 
                    Id =list.Id.ToString();
                    Title=list.Title;
                    Priority=list.Priority;
                    Completed=list.Completed;
                }
                b (ilistRepository.CreateList(a))
                
            member this.GetAllList(_)  = 
                let unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                let a = fun (list: DBType.ToDoListModel) -> { 
                    Id =list._id.ToString();
                    Title=list.Title;Priority=list.Priority;
                    Completed=list.Done;
                }
                ilistRepository.GetAll()|>List.ofSeq|> List.map(a)
           
            member this.DeleteListBy(listId:string) =
                ilistRepository.DeleteById(listId)

            member this.DeleteListCompleted(_) =
                ilistRepository.DeleteCompleted()

            member this.GetListBy(listId:string) =
                ilistRepository.GetList(listId)

            member this.UpdateList(toUpdateList:UpdateList) =
                let list = new  ToDoListEntity(toUpdateList.Id,toUpdateList.Title)
                list.Completed<-toUpdateList.Completed
                list.Priority<-toUpdateList.Priority
                ilistRepository.UpdateList(list)

            member this.UpdateListCheckAll(checks:CheckAll) =
                ilistRepository.UpdateListCheckAll(checks.Completed)
                
