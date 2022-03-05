module DBType
    open MongoDB.Bson

    type ToDoListModel ={
        _id: ObjectId
        CreateTimeStamp: BsonTimestamp
        Title: string
        Reminder: BsonTimestamp 
        CategoryId: string 
        Done :bool
        Priority:int
        }

    type CategoryModel = {
        _id: ObjectId
        ListItem:ToDoListModel[]
        Name:string
        }