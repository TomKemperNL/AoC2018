module AoC2018.Tests.Day2

open NUnit.Framework
open System.IO
open System
open Day2

[<TestFixture>]
type Day2Tests() =
       
    [<Test>]
    member this.Day2AExample() =        
        Assert.AreEqual(12, day2A (readFileLines "./Day2Example.txt"))
    
    [<Test>]
    member this.Day2BExample() =        
        Assert.AreEqual("fgij", day2B (readFileLines "./Day2Example.txt"))


    [<Test>]
    member this.Day2A() =        
        Assert.AreEqual(3952, day2A (readFileLines "./Day2Input.txt"))

    [<Test>]
    member this.Day2B() =        
        Assert.AreEqual("vtnikorkulbfejvyznqgdxpaw", day2B (readFileLines "./Day2Input.txt"))