module Day7Tests

open NUnit.Framework
open System.IO
open System
open Day7

[<TestFixture>]
type Day7Tests() =

    [<Test>]
    member this.Day7AExample() =       
        let input : string[] = [| 
            "Step C must be finished before step A can begin.";
            "Step C must be finished before step F can begin.";
            "Step A must be finished before step B can begin.";
            "Step A must be finished before step D can begin.";
            "Step B must be finished before step E can begin.";
            "Step D must be finished before step E can begin.";
            "Step F must be finished before step E can begin." |]
        Assert.AreEqual("CABDFE", day7A input)

    [<Test>]
    member this.Day7A() =
        let path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./Day7Input.txt")
        Assert.AreEqual("BHRTWCYSELPUVZAOIJKGMFQDXN", (day7A (File.ReadAllLines(path))))
   
   
    [<Test>]
    member this.Day7BExample() =
        let input : string[] = [| 
            "Step C must be finished before step A can begin.";
            "Step C must be finished before step F can begin.";
            "Step A must be finished before step B can begin.";
            "Step A must be finished before step D can begin.";
            "Step B must be finished before step E can begin.";
            "Step D must be finished before step E can begin.";
            "Step F must be finished before step E can begin." |]
        let result = day7B 2 0 input
        Assert.AreEqual(15, result)


    [<Test>]
    member this.Day7B() =
        let path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./Day7Input.txt")
        Assert.AreEqual(959, (day7B 5 60 (File.ReadAllLines(path))))
