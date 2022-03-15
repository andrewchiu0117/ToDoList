namespace ToDoList
open System
 [<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
module ContractModel =

    type CreateList = {
        Title : string
    }

    type UpdateList = {
        Id :string
        Title : string
        Priority :int
        Completed: bool
    }

    type ListDetail= {
        Id :string
        Title : string
        Priority :int
        Completed: bool
    }

    type CheckAll = {
        Completed : bool
    }

    type DeleteCompleteList = {
        Id :string
    }