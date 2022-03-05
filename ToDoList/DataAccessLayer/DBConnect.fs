module DBConnect

    open MongoDB.Bson
    open MongoDB.Driver

    type DBConnection(connectionString:string, dataBaseName:string) = 
        let client = new MongoClient(connectionString)
        let server = client.GetServer()
        let db = server.GetDatabase(dataBaseName)
        member this.GetDB = db
