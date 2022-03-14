namespace ToDoList

open ContractModel
open Microsoft.FSharp.Linq
open Entity
open IRepository
open Repository
open System
open IService

// static let ilistService = ListService(new ListRepository()) :> IListService<ListRepository>

module Service =
    //let ilistRepository = ListRepository() :> IListRepository
    type ListService(ilistRepository : IListRepository )=
        interface IListService<ListRepository> with 
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
                    //CreateTimeStamp = unixEpoch.AddSeconds(float list.CreateTimeStamp.Value);
                    //Reminder = unixEpoch.AddSeconds(float list.Reminder.Value)
                }
                b (ilistRepository.CreateList(a))
                
            member this.GetAllList(_)  = 
                let unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                let a = fun (list: DBType.ToDoListModel) -> { 
                    Id =list._id.ToString();
                    Title=list.Title;Priority=list.Priority;
                    Completed=list.Done;
                //CreateTimeStamp = unixEpoch.AddSeconds(float list.CreateTimeStamp.Value);
                //Reminder = unixEpoch.AddSeconds(float list.Reminder.Value)
                }
                ilistRepository.GetAll()|>List.ofSeq|> List.map(a)
           
            member this.DeleteListBy(listId:string) =
                ilistRepository.DeleteById(listId)

            member this.DeleteListCompleted(_) =
                ilistRepository.DeleteCompleted()

            member this.getListBy(listId:string) =
                ilistRepository.GetList(listId)

            member this.UpdateList(toUpdateList:UpdateList) =
                let list = new  ToDoListEntity(toUpdateList.Id,toUpdateList.Title)
                list.Completed<-toUpdateList.Completed
                list.Priority<-toUpdateList.Priority
                ilistRepository.UpdateList(list)

            member this.UpdateListCheckAll(checks:CheckAll) =
                ilistRepository.UpdateListCheckAll(checks.Completed)
                
