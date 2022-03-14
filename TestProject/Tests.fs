namespace TestProject
open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open ToDoList
open DBType
open MongoDB.Driver

[<TestClass>]
type TestListEntity () =
    
    [<TestMethod>]
    member this.TestListEntity_A_Entity_Priority () =
        let item = new Entity.ToDoListEntity ("","UnitTest")
        let mutable isException = false
        try
            item.SetPriority(-1)
        with
            | OuterError(str) -> isException <- true

        if isException = true then
            Assert.IsTrue(true)
        else
            Assert.IsTrue(false)

    [<TestMethod>]
    member this.TestListEntity_B_Entity_Priority () =
        let item = new Entity.ToDoListEntity ("","UnitTest")
        let mutable isException = false
        try
            item.SetPriority(5)
        with
            | OuterError(str) -> isException <- true

        if isException = true then
            Assert.IsTrue(true)
        else
            Assert.IsTrue(false)

    [<TestMethod>]
    member this.TestListEntity_C_Entity_Title () =
        let item = new Entity.ToDoListEntity ("","UnitTest")
        Assert.IsTrue(item.Title = "UnitTest")

    [<TestMethod>]
    member this.TestListEntity_D_Entity_Title () =
        let item = new Entity.ToDoListEntity ("123","UnitTest")
        Assert.IsTrue(item.Id = "123")

[<TestClass>]
type TestListRepository () =
    
    [<TestMethod>]
    member this.TestListRepository_A_GetList () =
        let ilistRepository = MockRepository.MockListRepository() :> IRepository.IListRepository
        let ans = ilistRepository.GetList("666")
        Assert.IsTrue(ans.Id = "123")
    
    [<TestMethod>]
    member this.TestListRepository_B_CreateList () =
        let ilistRepository = MockRepository.MockListRepository() :> IRepository.IListRepository
        let item = new Entity.ToDoListEntity ("","UnitTest")
        let ans = ilistRepository.CreateList(item)
        Assert.IsTrue(ans.Title = "Testing")
    
    [<TestMethod>]
    member this.TestListRepository_C_GetAll () =
        let ilistRepository = MockRepository.MockListRepository() :> IRepository.IListRepository
        let ans = Seq.toList(ilistRepository.GetAll())
        Assert.IsTrue(ans.GetType() = typeof<ToDoListModel list>);

    [<TestMethod>]
    member this.TestListRepository_D_GetByTitle () =
        let ilistRepository = MockRepository.MockListRepository() :> IRepository.IListRepository
        let ans = Seq.toList(ilistRepository.GetByTitle(""))
        Assert.IsTrue(ans.GetType() = typeof<ToDoListModel list>);

    [<TestMethod>]
    member this.TestListRepository_E_GetById () =
        let ilistRepository = MockRepository.MockListRepository() :> IRepository.IListRepository
        let ans = Seq.toList(ilistRepository.GetById(""))
        Assert.IsTrue(ans.GetType() = typeof<ToDoListModel list>);

    [<TestMethod>]
    member this.TestListRepository_F_UpdateList () =
        let ilistRepository = MockRepository.MockListRepository() :> IRepository.IListRepository
        let item = new Entity.ToDoListEntity ("","UnitTest")
        let ans = ilistRepository.UpdateList(item)
        Assert.IsTrue(ans.GetType() = typeof<WriteConcernResult>);

    [<TestMethod>]
    member this.TestListRepository_G_DeleteById () =
        let ilistRepository = MockRepository.MockListRepository() :> IRepository.IListRepository
        let ans = ilistRepository.DeleteById("")
        Assert.IsTrue(ans.GetType() = typeof<WriteConcernResult>);

    [<TestMethod>]
    member this.TestListRepository_H_DeleteCompleted () =
        let ilistRepository = MockRepository.MockListRepository() :> IRepository.IListRepository
        let ans = ilistRepository.DeleteCompleted()
        Assert.IsTrue(ans.GetType() = typeof<WriteConcernResult>);


// Warning: This unit test will need connection to Database
[<TestClass>]
type TestDBConnection () =
    
    [<TestMethod>]
    member this.TestDB_A_Insert () =
        let item = new Entity.ToDoListEntity ("","UnitTest")
        let ilistRepository = Repository.ListRepository() :> IRepository.IListRepository
        let ans = ilistRepository.CreateList(item)
        Assert.IsTrue(ans.Title = "UnitTest");

    [<TestMethod>]
    member this.TestDB_B_FindByTitle () =
        let ilistRepository = Repository.ListRepository() :> IRepository.IListRepository
        let ans = Seq.toList(ilistRepository.GetByTitle("UnitTest"))
        Assert.IsTrue(ans[0].Title = "UnitTest");

    [<TestMethod>]
    member this.TestDB_C_FindAll () =
        let ilistRepository = Repository.ListRepository() :> IRepository.IListRepository
        let ans = Seq.toList(ilistRepository.GetAll())
        let mutable finded = false
        for item in ans do
            if item.Title = "UnitTest" then
                finded <- true
        Assert.IsTrue(finded);

    [<TestMethod>]
    member this.TestDB_D_FindUTID () =
        let ilistRepository = Repository.ListRepository() :> IRepository.IListRepository
        let temp = Seq.toList(ilistRepository.GetByTitle("UnitTest"))
        let ans = Seq.toList(ilistRepository.GetById(temp[0]._id.ToString()))
        Assert.IsTrue(ans[0].Title = "UnitTest");

    [<TestMethod>]
    member this.TestDB_E_Update () =
        let ilistRepository = Repository.ListRepository() :> IRepository.IListRepository
        let temp = Seq.toList(ilistRepository.GetByTitle("UnitTest"))
        let item = new Entity.ToDoListEntity (temp[0]._id.ToString(),"UnitTest")
        item.SetPriority(2)
        ilistRepository.UpdateList(item) |>ignore
        let ans = Seq.toList(ilistRepository.GetByTitle("UnitTest"))
        Assert.IsTrue(ans[0].Priority = 2);

    [<TestMethod>]
    member this.TestDB_F_Delete () =
        let ilistRepository = Repository.ListRepository() :> IRepository.IListRepository
        let temp = Seq.toList(ilistRepository.GetByTitle("UnitTest"))
        ilistRepository.DeleteById(temp[0]._id.ToString()) |>ignore
        let ans = Seq.toList(ilistRepository.GetByTitle("UnitTest"))
        Assert.IsTrue(ans.Length = 0);


