namespace ToDoList

open Entity
open MongoDB.Driver
open DBType


module IRepository=
    type IListRepository=
        abstract member GetList: string -> ToDoListEntity
        abstract member CreateList: ToDoListEntity -> ToDoListEntity
        abstract member GetAll: unit -> ToDoListModel seq
        abstract member GetByTitle: string -> ToDoListModel seq
        abstract member GetById: string -> ToDoListModel seq
        abstract member UpdateList: ToDoListEntity -> WriteConcernResult
        abstract member UpdateListCheckAll: bool -> unit
        abstract member DeleteById: string -> WriteConcernResult
        abstract member DeleteCompleted: unit -> WriteConcernResult

            
        
