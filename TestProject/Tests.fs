namespace TestProject
open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open ToDoList


[<TestClass>]
type TestModel () =
    
    [<TestMethod>]
    member this.TestDB_A_Insert () =
        let item = new Entity.ToDoListEntity ("","UnitTest")
        let listRepository = new Repository.ListRepository()
        let ans = listRepository.CreateList(item)
        Assert.IsTrue(ans.Title = "UnitTest");

    [<TestMethod>]
    member this.TestDB_B_FindByTitle () =
        let listRepository = new Repository.ListRepository()
        let ans = listRepository.GetByTitle("UnitTest")
        Assert.IsTrue(ans[0].Title = "UnitTest");

    [<TestMethod>]
    member this.TestDB_C_FindAll () =
        let listRepository = new Repository.ListRepository()
        let ans = listRepository.GetAll()
        let mutable finded = false
        for item in ans do
            if item.Title = "UnitTest" then
                finded <- true
        Assert.IsTrue(finded);

    [<TestMethod>]
    member this.TestDB_D_FindUTID () =
        let listRepository = new Repository.ListRepository()
        let temp = listRepository.GetByTitle("UnitTest")
        let ans = listRepository.GetById(temp[0]._id.ToString())
        Assert.IsTrue(ans[0].Title = "UnitTest");

    [<TestMethod>]
    member this.TestDB_E_Update () =
        let listRepository = new Repository.ListRepository()
        let temp = listRepository.GetByTitle("UnitTest")
        let item = new Entity.ToDoListEntity (temp[0]._id.ToString(),"UnitTest")
        item.SetPriority(2)
        listRepository.UpdateList(item) |>ignore
        let ans = listRepository.GetByTitle("UnitTest")
        Assert.IsTrue(ans[0].Priority = 2);

    [<TestMethod>]
    member this.TestDB_F_Delete () =
        let listRepository = new Repository.ListRepository()
        let temp = listRepository.GetByTitle("UnitTest")
        listRepository.DeleteById(temp[0]._id.ToString()) |>ignore
        let ans = listRepository.GetByTitle("UnitTest")
        Assert.IsTrue(ans.Count = 0);


[<TestClass>]
type TestController () =
    
    [<TestMethod>]
    member this.TestController_A_Entity_Priority () =
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
    member this.TestController_B_Entity_Priority () =
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
    member this.TestController_C_Entity_Title () =
        let item = new Entity.ToDoListEntity ("","UnitTest")
        Assert.IsTrue(item.Title = "UnitTest")

    [<TestMethod>]
    member this.TestController_D_Entity_Title () =
        let item = new Entity.ToDoListEntity ("123","UnitTest")
        Assert.IsTrue(item.Id = "123")