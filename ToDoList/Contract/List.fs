namespace ToDoList
open System
module ContractModel =

    type CreateList = {
        Title : string
    }

    type UpdateList = {
        Id :string
        Title : string
        Priority :int
        Reminder : DateTime
        Completed: bool
    }


