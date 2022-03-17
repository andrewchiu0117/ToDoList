namespace TestProject
open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open ToDoList
open DBType
open MongoDB.Driver
open System.Threading
open MongoDB.Bson


[<TestClass>]
type TestListService () =
    [<TestMethod>]
    member this.TestListService_A_ToListDetail () =
        let ilistService = Service.ListService(new MockRepository.MockListRepository()) :> IService.IListService<IRepository.IListRepository>
        let todoListModel : DBType.ToDoListModel = {
                                                _id= ObjectId("622fff0b9b49ea7061db26ba");
                                                Title = "title";
                                                Done = true;
                                                Priority = 1}
        let returnListDetail = ilistService.ToListDetail(todoListModel)
        Assert.IsTrue(returnListDetail.Id = "622fff0b9b49ea7061db26ba" && returnListDetail.Title = "title" && returnListDetail.Priority = 1 && returnListDetail.Completed = true)

    [<TestMethod>]
    member this.TestListService_B_Create () =
        let ilistService = Service.ListService(new MockRepository.MockListRepository()) :> IService.IListService<IRepository.IListRepository>
        let createList : ContractModel.CreateList = { Title = "title"}
        let returnListDetail = ilistService.Create(createList)
        Assert.IsTrue(returnListDetail.Id = "123" && returnListDetail.Title = "Testing" && returnListDetail.Priority = 0 && returnListDetail.Completed = false)

    [<TestMethod>]
    member this.TestListService_C_GetAllList () =
        let ilistService = Service.ListService(new MockRepository.MockListRepository()) :> IService.IListService<IRepository.IListRepository>
        let returnValue = ilistService.GetAllList()
        Assert.IsTrue(returnValue.Length = 0)

    [<TestMethod>]
    member this.TestListService_D_DeleteListBy () =
        let ilistService = Service.ListService(new MockRepository.MockListRepository()) :> IService.IListService<IRepository.IListRepository>
        let returnValue = ilistService.DeleteListBy("123")
        Assert.IsTrue(returnValue.Response.ToString() = "{ }")

    [<TestMethod>]
    member this.TestListService_E_DeleteListCompleted () =
        let ilistService = Service.ListService(new MockRepository.MockListRepository()) :> IService.IListService<IRepository.IListRepository>
        let returnValue = ilistService.DeleteListCompleted()
        Assert.IsTrue(returnValue.Response.ToString() = "{ }")

    [<TestMethod>]
    member this.TestListService_F_getListBy () =
        let ilistService = Service.ListService(new MockRepository.MockListRepository()) :> IService.IListService<IRepository.IListRepository>
        let returnValue = ilistService.GetListBy("123")
        Assert.IsTrue(returnValue.Id = "123" && returnValue.Title = "Testing" && returnValue.Priority = 0 && returnValue.Completed = false)

    [<TestMethod>]
    member this.TestListService_G_UpdateList () =
        let ilistService = Service.ListService(new MockRepository.MockListRepository()) :> IService.IListService<IRepository.IListRepository>
        let updateList : ContractModel.UpdateList = {
                                                Id= "123";
                                                Title = "title";
                                                Completed = true;
                                                Priority = 1}
        let returnValue = ilistService.UpdateList(updateList)
        Assert.IsTrue(returnValue.Response.ToString() = "{ }")

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
    member this.TestListEntity_D_Entity_ID () =
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


[<TestClass>]
type TestContractModel () =
    
    [<TestMethod>]
    member this.TestContractModel_A_CreateList () =
        let item :  ContractModel.CreateList={Title="123"}
        Assert.IsTrue(item.Title = "123")

    [<TestMethod>]
    member this.TestContractModel_B_UpdateList () =
        let item : ContractModel.UpdateList={
                                      Title="123";
                                      Id = "123";
                                      Priority = 1;
                                      Completed = true;}
        Assert.IsTrue(item.Title = "123" && item.Id = "123" && item.Priority = 1 && item.Completed = true)

    [<TestMethod>]
    member this.TestContractModel_C_ListDetail () =
        let item : ContractModel.ListDetail={
                                      Title="123";
                                      Id = "123";
                                      Priority = 1;
                                      Completed = true;}
        Assert.IsTrue(item.Title = "123" && item.Id = "123" && item.Priority = 1 && item.Completed = true)

    [<TestMethod>]
    member this.TestContractModel_D_CheckAll () =
        let item : ContractModel.CheckAll={Completed=true}
        Assert.IsTrue(item.Completed = true)

    [<TestMethod>]
    member this.TestContractModel_E_DeleteCompleteList () =
        let item : ContractModel.DeleteCompleteList={Id = "123"}
        Assert.IsTrue(item.Id = "123")



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
        Thread.Sleep(200)
        let ilistRepository = Repository.ListRepository() :> IRepository.IListRepository
        let ans = Seq.toList(ilistRepository.GetByTitle("UnitTest"))
        Assert.IsTrue(ans[0].Title = "UnitTest");

    [<TestMethod>]
    member this.TestDB_C_FindAll () =
        Thread.Sleep(250)
        let ilistRepository = Repository.ListRepository() :> IRepository.IListRepository
        let ans = Seq.toList(ilistRepository.GetAll())
        let mutable finded = false
        for item in ans do
            if item.Title = "UnitTest" then
                finded <- true
        Assert.IsTrue(finded);

    [<TestMethod>]
    member this.TestDB_D_FindUTID () =
        Thread.Sleep(300)
        let ilistRepository = Repository.ListRepository() :> IRepository.IListRepository
        let temp = Seq.toList(ilistRepository.GetByTitle("UnitTest"))
        let ans = Seq.toList(ilistRepository.GetById(temp[0]._id.ToString()))
        Assert.IsTrue(ans[0].Title = "UnitTest");

    [<TestMethod>]
    member this.TestDB_E_Update () =
        Thread.Sleep(350)
        let ilistRepository = Repository.ListRepository() :> IRepository.IListRepository
        let temp = Seq.toList(ilistRepository.GetByTitle("UnitTest"))
        let item = new Entity.ToDoListEntity (temp[0]._id.ToString(),"UnitTest")
        item.SetPriority(2)
        ilistRepository.UpdateList(item) |>ignore
        let ans = Seq.toList(ilistRepository.GetByTitle("UnitTest"))
        Assert.IsTrue(ans[0].Priority = 2);

    [<TestMethod>]
    member this.TestDB_F_Delete () =
        Thread.Sleep(1000)
        let ilistRepository = Repository.ListRepository() :> IRepository.IListRepository
        let temp = Seq.toList(ilistRepository.GetByTitle("UnitTest"))
        ilistRepository.DeleteById(temp[0]._id.ToString()) |>ignore
        let ans = Seq.toList(ilistRepository.GetByTitle("UnitTest"))
        Assert.IsTrue(ans.Length = 0);


