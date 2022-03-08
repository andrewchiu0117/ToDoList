namespace TestProject
open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open ToDoList


[<TestClass>]
type TestControllerToModel () =

    [<TestMethod>]
    member this.TestDBInsert () =
        //let controller = new ListController()
        //let createList = new ContractModel.CreateList
        //controller.Post(createList)
        let item = new Entity.ToDoListEntity ("123465789123465","Andrew0305")
        let listRepository = new Repository.ListRepository()
        let ans = listRepository.CreateList(item) |> ignore
        Assert.IsTrue(true);

    [<TestMethod>]
    member this.TestDBFindAll () =
        let listRepository = new Repository.ListRepository()
        //let ans = listRepository.GetById("62222a0b546b522cddb0e403")

        let ans2 = listRepository.GetAll()
        Assert.IsTrue(true);
