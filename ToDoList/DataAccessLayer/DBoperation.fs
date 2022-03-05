module DBoperation
    
    open MongoDB.Bson
    open MongoDB.Driver
    open MongoDB.Driver.Builders
    open MongoDB.FSharp
    open System
    exception InnerError of string
    exception OuterError of string

    [<Literal>]
    let ConnectionString = "mongodb://localhost"

    [<Literal>]
    let DbName = "ToDoList"

    [<Literal>]
    let CollectionName = "ToDoListModel"


    let db = new DBConnect.DBConnection(ConnectionString,DbName)
    let ToDoListModelCollection = db.GetDB.GetCollection<DBType.ToDoListModel> CollectionName



    type ToDoListEntity(title) =
        let mutable _Id  = Guid.NewGuid()
        let mutable _Title = title
        let mutable _Priority =0
        let mutable _CreateTimestamp = DateTime.Now
        let mutable _Reminder = DateTime.Now
        let mutable _Completed = false
        member this.SetReminder(dateTime)=
            if dateTime > _CreateTimestamp then _Reminder <- dateTime
            else raise (InnerError("outer"))
        member this.Completed with get()=_Completed and set(value)=_Completed<-value
        member this.Priority with get()=_Priority
        member this.Id with get()=_Id
        member this.Title with get()=_Title
        member this.CreateTimestamp with get()=_CreateTimestamp
        member this.Reminder with get()=_Reminder
        member this.Setpriority (p)=
            if(p>4 ||p<1)then raise (OuterError("outer"))
            else
                _Priority<-p


    let Insert_ToDoListModel (insertList:ToDoListEntity)= 
        let id = ObjectId.GenerateNewId()
        ToDoListModelCollection.Insert{ 
            DBType.ToDoListModel._id = id
            DBType.ToDoListModel.CreateTimeStamp = BsonTimestamp(DateTimeOffset(insertList.CreateTimestamp).ToUnixTimeSeconds()); 
            DBType.ToDoListModel.Title = insertList.Title; 
            DBType.ToDoListModel.Reminder = BsonTimestamp(DateTimeOffset(insertList.Reminder).ToUnixTimeSeconds());
            DBType.ToDoListModel.CategoryId = insertList.Id.ToString();
            DBType.ToDoListModel.Done=insertList.Completed;
            DBType.ToDoListModel.Priority=insertList.Priority }


    let Select_ToDoListModel_One(ids:string) = 
        let value = BsonString(ids)
        ToDoListModelCollection.Find(Query.GTE("Title",value))

    let Select_ToDoListModel_All() = 
        ToDoListModelCollection.Find(Query.Empty)

    let Select_ToDoListModel_Title (titleString : string) = 
        ToDoListModelCollection.FindAll()


    let readOnId ( id : BsonObjectId ) = 
        ToDoListModelCollection.FindOneById(id)