﻿namespace ToDoList
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
        Editing:bool
    }

    type ListDetail= {
        Id :string
        Title : string
        Priority :int
        Completed: bool
        Editing:bool
    }