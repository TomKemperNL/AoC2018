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
        Assert.AreEqual(240, day4A (readFileLines "./Day4Example.txt"))
    
    [<Test>]
    member this.Day4A() =        
        Assert.AreEqual(39698, day4A (readFileLines "./Day4Input.txt"))

    
    [<Test>]
    member this.Day4BExample() =        
        Assert.AreEqual(4455, day4B (readFileLines "./Day4Example.txt"))
    
    [<Test>]
    member this.Day4B() =        
        Assert.AreEqual(14920, day4B (readFileLines "./Day4Input.txt"))
