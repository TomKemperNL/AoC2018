module Day5Tests

open NUnit.Framework
open System.IO
open System
open Day5


[<TestFixture>]
type Day5Tests() =

    [<Test>]
    member this.Day5AExample() =        
        Assert.AreEqual("dabCBAcaDA".Length, day5A "dabAcCaCBAcCcaDA")

    [<Test>]
    member this.Day5ASmallExamples() =        
        Assert.AreEqual(0, day5A "aA")
        Assert.AreEqual(0, day5A "abBA")
        Assert.AreEqual(4, day5A "abAB")
        Assert.AreEqual(6, day5A "aabAAB")
    
    [<Test>]
    member this.Day5A() =        
        Assert.AreEqual(11364, (day5A (readFile "./Day5Input.txt")))

    [<Test>]
    member this.Day5B() =        
        let result = day5B (readFile "./Day5Input.txt")
        Assert.AreEqual(4212, result)

