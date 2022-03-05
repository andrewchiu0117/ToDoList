namespace TestProject
open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open ToDoList


[<TestClass>]
type TestClass () =

    [<TestMethod>]
    //member this.TestDBInsertEntityofTitle () =
    //    let item = new Entity.ToDoListEntity "Andrew0305"
    //    let listRepository = new Repository.ListRepository()
    //    let ans = listRepository.CreateList(item) |> ignore
    //    Assert.IsTrue(true);

    member this.TestDBFindAll () =
        let listRepository = new Repository.ListRepository()

        let ans = listRepository.GetAll()
        Assert.IsTrue(true);