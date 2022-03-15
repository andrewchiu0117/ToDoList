namespace ToDoList

open ContractModel
open Entity

module IService =
    type IListService<'T>=

        abstract member ToListDetail: DBType.ToDoListModel-> ListDetail
            
        abstract member Create: CreateList-> ListDetail

        abstract member GetAllList: _ -> ListDetail list

        abstract member DeleteListBy: string -> MongoDB.Driver.WriteConcernResult

        abstract member DeleteListCompleted: _ -> MongoDB.Driver.WriteConcernResult

        abstract member GetListBy: string -> ToDoListEntity

        abstract member UpdateList: UpdateList -> MongoDB.Driver.WriteConcernResult

        abstract member UpdateListCheckAll: CheckAll -> unit
