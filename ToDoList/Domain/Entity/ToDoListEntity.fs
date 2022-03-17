namespace ToDoList
exception OuterError of string

open System
module Entity=
    type ToDoListEntity(id :string ,title: string)  =
        let mutable _Id = id :string
        let mutable _Title = title : string
        let mutable _Priority =0
        let mutable _Completed = false

        member this.Completed with get()=_Completed and set(value)= _Completed<-value

        member this.Priority with get()=_Priority and set(value)= _Priority<-value

        member this.Id with get()=_Id 

        member this.Title with get()= _Title 

        member this.SetPriority (p)=
            if(p>4 ||p<0) then raise (OuterError("outer"))
            else 
                _Priority<-p
        
