namespace ToDoList

exception InnerError of string
exception OuterError of string

open System
module Entity=
    type ToDoListEntity(id :string ,title: string)  =
        let mutable _Id = id :string
        let mutable _Title = title : string
        let mutable _Priority =0
        let mutable _CreateTimeStamp  = DateTime.Now
        let mutable _Reminder  = DateTime.Now
        let mutable _Completed = false

        member this.SetReminder(dateTime) = 
            if dateTime > _CreateTimeStamp then _Reminder <- dateTime
            else raise (InnerError("outer"))

        member this.Completed with get()=_Completed and set(value)= _Completed<-value

        member this.Priority with get()=_Priority and set(value)= _Priority<-value

        member this.Id with get()=_Id 

        member this.Title with get()= _Title 

        member this.CreateTimeStamp with get()=_CreateTimeStamp 

        member this.Reminder with get()=_Reminder 

        member this.SetPriority (p)=
            if(p>4 ||p<1) then raise (OuterError("outer"))
            else 
                _Priority<-p
        
