module Day4Tests

open NUnit.Framework
open System.IO
open System
open Day4
open System.Globalization

[<TestFixture>]
type Day4Tests() =

    [<Test>]
    member this.Day4AExample() =
        let path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./Day4Example.txt")
        Assert.AreEqual(240, day4A (File.ReadAllLines(path)))
    
    [<Test>]
    member this.Day4A() =
        let path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./Day4Input.txt")
        Assert.AreEqual(39698, day4A (File.ReadAllLines(path)))

    
    [<Test>]
    member this.Day4BExample() =
        let path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./Day4Example.txt")
        Assert.AreEqual(4455, day4B (File.ReadAllLines(path)))
    
    [<Test>]
    member this.Day4B() =
        let path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./Day4Input.txt")
        Assert.AreEqual(14920, day4B (File.ReadAllLines(path)))
