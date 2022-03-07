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
        Completed: bool
        CreateTimeStamp : DateTime
        Reminder :DateTime
    }

    type ListDetail= {
        Id :string
        Title : string
        Priority :int
        Completed: bool
        CreateTimeStamp : DateTime
        Reminder :DateTime
    }