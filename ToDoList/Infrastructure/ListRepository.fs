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

module Repository=
    type ListRepository()=
      
        [<Literal>]
        let ConnectionString = "mongodb://localhost"
        [<Literal>]
        let DbName = "ToDoList"
        [<Literal>]
        let CollectionName = "ToDoListModel"

        let client         = MongoClient(ConnectionString)
        let db = new DBConnect.DBConnection(ConnectionString, DbName)
        let ToDoListModelCollection = db.GetDB.GetCollection<DBType.ToDoListModel> CollectionName

        interface IListRepository with
            member this.GetList(id:string)= 
                let bsonId = BsonObjectId(ObjectId(id))
                let a = ToDoListModelCollection.FindOneById(bsonId)
                let result = new ToDoListEntity(a._id.ToString(),a.Title)
                result.Completed<-a.Done
                result.Priority<-a.Priority
                result
            
            member this.CreateList(list :ToDoListEntity) = 
                let id = ObjectId.GenerateNewId()
                ToDoListModelCollection.Insert{
                    _id=id
                    CreateTimeStamp = BsonTimestamp(DateTimeOffset(list.CreateTimeStamp).ToUnixTimeSeconds())
                    Title = list.Title
                    Reminder = BsonTimestamp(DateTimeOffset(list.Reminder).ToUnixTimeSeconds())
                    CategoryId = list.Id.ToString()
                    Done=list.Completed
                    Priority=list.Priority 
                }|>ignore
                let result = new ToDoListEntity(id.ToString(),list.Title)
                result
            
            member this.GetAll() = 
                ToDoListModelCollection.Find(Query.Empty).ToList()

            member this.GetByTitle(titleName:string) = 
                let value = BsonString(titleName)
                ToDoListModelCollection.Find(Query.EQ("Title", value)).ToList()

            member this.GetById(id:string) = 
                let bsonId = BsonObjectId(ObjectId(id))
                ToDoListModelCollection.Find(Query.EQ("_id", bsonId)).ToList()

            member this.UpdateList(list :ToDoListEntity) = 
                let filter           = QueryBuilder<DBType.ToDoListModel>().EQ((fun x -> x._id), ObjectId(list.Id.ToString()))
                let updateDefinition = UpdateBuilder<DBType.ToDoListModel>()
                                        .Set((fun x -> x.Title),list.Title)
                                        .Set((fun x -> x.Done),list.Completed)
                                        .Set((fun x -> x.Priority),list.Priority)
                ToDoListModelCollection.Update(filter,updateDefinition)

            member this.UpdateListCheckAll(checks :bool) = 
                let allList = ToDoListModelCollection.Find(Query.Empty).ToList()
                for item in allList do
                    let filter           = QueryBuilder<DBType.ToDoListModel>().EQ((fun x -> x._id),  ObjectId(item._id.ToString()))
                    let updateDefinition = UpdateBuilder<DBType.ToDoListModel>()
                                            .Set((fun x -> x.Done),checks)
                    ToDoListModelCollection.Update(filter,updateDefinition) |> ignore

            member this.DeleteById(id:string) = 
                let filter           = QueryBuilder<DBType.ToDoListModel>().EQ((fun x -> x._id), ObjectId(id))
                ToDoListModelCollection.Remove(filter)

            member this.DeleteCompleted() = 
                let filter           = QueryBuilder<DBType.ToDoListModel>().EQ((fun x -> x.Done), true)
                ToDoListModelCollection.Remove(filter)

            
        
