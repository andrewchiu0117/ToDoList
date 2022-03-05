namespace ToDoList

open Entity

open MongoDB.Driver
open DBType
open MongoDB.Bson
open System
open MongoDB.Driver
open MongoDB.Driver.Builders
open MongoDB.FSharp

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

        let mutable toDoLists : ToDoListEntity list =[]

        member this.GetList(id:string)=
            Array.find(fun item-> ((ToDoListEntity)item).Id.ToString().Equals(id)) 

        member this.CreateList(list :ToDoListEntity) = 
            let id = ObjectId.GenerateNewId()
            ToDoListModelCollection.Insert{
                DBType.ToDoListModel._id = id
                DBType.ToDoListModel.CreateTimeStamp = BsonTimestamp(DateTimeOffset(list.CreateTimeStamp).ToUnixTimeSeconds()); 
                DBType.ToDoListModel.Title = list.Title; 
                DBType.ToDoListModel.Reminder = BsonTimestamp(DateTimeOffset(list.Reminder).ToUnixTimeSeconds());
                DBType.ToDoListModel.CategoryId = list.Id.ToString();
                DBType.ToDoListModel.Done=list.Completed;
                DBType.ToDoListModel.Priority=list.Priority 
            }
        
        member this.GetAll() = 
            ToDoListModelCollection.Find(Query.Empty)

        member this.GetByTitle(titleName:string) = 
            let value = BsonString(titleName)
            ToDoListModelCollection.Find(Query.GTE("Title", value))

        member this.GetById(id:string) = 
            let bsonId = BsonObjectId(ObjectId(id))
            ToDoListModelCollection.Find(Query.EQ("_id", bsonId))

        //member this.DeleteById(id:string) = 
        //       Array.re

        //member this.
