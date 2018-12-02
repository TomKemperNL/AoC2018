module AoC2018.Tests.Day2

open NUnit.Framework
open System.IO
open System
open Day2

[<TestFixture>]
type Day2Tests() =
       
    [<Test>]
    member this.Day2AExample() =
        let path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./Day2Example.txt")
        Assert.AreEqual(12, day2A (File.ReadAllLines(path)))
    
    [<Test>]
    member this.Day2BExample() =
        let path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./Day2BExample.txt")
        Assert.AreEqual("fgij", day2B (File.ReadAllLines(path)))


    [<Test>]
    member this.Day2A() =
        let path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./Day2Input.txt")
        Assert.AreEqual(3952, day2A (File.ReadAllLines(path)))

    [<Test>]
    member this.Day2B() =
        let path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./Day2Input.txt")
        Assert.AreEqual("fgij", day2B (File.ReadAllLines(path)))