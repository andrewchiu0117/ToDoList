namespace ToDoList

open ContractModel
open Microsoft.FSharp.Linq
open Entity
open Repository
open System

module Service =
   type ListService(listRepository :ListRepository)= 
        
         member this.ToListDateil(list: DBType.ToDoListModel) =
                let unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                fun n -> { 
                    Id =list._id.ToString();
                    Title=list.Title;Priority=list.Priority;
                    Completed=list.Done;
                    Editing = true;
                    //CreateTimeStamp = unixEpoch.AddSeconds(float list.CreateTimeStamp.Value);
                    //Reminder = unixEpoch.AddSeconds(float list.Reminder.Value)
                    }

           
            
         member this.Create(list: CreateList) : string  = 
                let a = new  ToDoListEntity("",list.Title)
                listRepository.CreateList(a)

  
                
         member this.GetAllList()  = 
                let unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                let a = fun (list: DBType.ToDoListModel) -> { 
                    Id =list._id.ToString();
                    Title=list.Title;Priority=list.Priority;
                    Completed=list.Done;
                    Editing = false
                    //CreateTimeStamp = unixEpoch.AddSeconds(float list.CreateTimeStamp.Value);
                    //Reminder = unixEpoch.AddSeconds(float list.Reminder.Value)
                    }
                listRepository.GetAll()|>List.ofSeq|> List.map(a)
                 
           
         member this.DeleteListBy(listId:string) =
                listRepository.DeleteById(listId)
            

         member this.getListBy(listId:string) =
            listRepository.GetList(listId)
            


         member this.UpdateList(toUpdateList:UpdateList) =
                let list = new  ToDoListEntity(toUpdateList.Id,toUpdateList.Title)
                list.Completed<-toUpdateList.Completed
                list.Priority<-toUpdateList.Priority
                listRepository.UpdateList(list)
                
         