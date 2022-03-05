namespace ToDoList

open ContractModel

open Entity
open Repository

module Service =
   type ListService(listRepository :ListRepository)= 
         
         member this.Create(list: CreateList) : string  = 
                let a = new  ToDoListEntity(list.Title)
                listRepository.CreateList(a)
                a.Id.ToString()
  
                
         member this.GetAllList()  = listRepository.GetAll()

        // member this.DeleteListBy(listId:string) =
            

         member this.getListBy(listId:string) =
            listRepository.GetList(listId)
            


         //member this.UpdateList(listId:UpdateList) =  
                
         