namespace ToDoList

open Entity
open MongoDB.Driver
open DBType
open MongoDB.Bson
open System
open MongoDB.Driver
open MongoDB.Driver.Builders
open MongoDB.FSharp
open MongoDB.Bson.Serialization
open FSharp.Json
open System.Linq
open IRepository

module MockRepository=
    type MockListRepository()=

        interface IListRepository with
            member this.GetList(id:string)= 
                let result = new ToDoListEntity("123","Testing")
                result
            
            member this.CreateList(list :ToDoListEntity) = 
                let result = new ToDoListEntity("123","Testing")
                result
            
            member this.GetAll() = 
                let result = []
                result

            member this.GetByTitle(titleName:string) = 
                let result = []
                result

            member this.GetById(id:string) = 
                let result = []
                result

            member this.UpdateList(list :ToDoListEntity) = 
                let result = new WriteConcernResult(new BsonDocument())
                result

            member this.UpdateListCheckAll(checks :bool) = 
                let result = ()
                result

            member this.DeleteById(id:string) = 
                let result = new WriteConcernResult(new BsonDocument())
                result

            member this.DeleteCompleted() = 
                let result = new WriteConcernResult(new BsonDocument())
                result

            
        
