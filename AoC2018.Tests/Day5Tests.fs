module Day5Tests

open NUnit.Framework
open System.IO
open System
open Day5


[<TestFixture>]
type Day5Tests() =

    [<Test>]
    member this.Day5AExample() =        
        Assert.AreEqual("dabCBAcaDA", day5A "dabAcCaCBAcCcaDA")

    [<Test>]
    member this.Day5ASmallExamples() =        
        Assert.AreEqual(String.Empty, day5A "aA")
        Assert.AreEqual(String.Empty, day5A "abBA")
        Assert.AreEqual("abAB", day5A "abAB")
        Assert.AreEqual("aabAAB", day5A "aabAAB")
    
    [<Test>]
    member this.Day5A() =
        let path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./Day5Input.txt")
        Assert.AreEqual(-1, day5A (File.ReadAllText(path)))

    [<Test>]
    member this.Day5B() =
        let path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./Day5Input.txt")
        Assert.AreEqual(-1, day5B (File.ReadAllLines(path)))

