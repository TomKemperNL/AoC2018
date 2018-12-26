module AoC2018.Tests.Day3

open NUnit.Framework
open System.IO
open System
open Day3
open TwoD

[<TestFixture>]
type Day3Tests() =

    [<Test>]
    member this.Day3Parse() = 
        let expected = Claim (12, Coord (2,4), Size (5,6))
        Assert.AreEqual(expected, parse "#12 @ 2,4: 5x6")
       
    [<Test>]
    member this.Day3AExample() =        
        Assert.AreEqual(4, day3A (readFileLines "./Day3AExample.txt"))
    
    [<Test>]
    member this.Day3A() =        
        Assert.AreEqual(114946, day3A (readFileLines "./Day3Input.txt"))

    
    [<Test>]
    member this.Day3BExample() =        
        Assert.AreEqual(3, day3B (readFileLines "./Day3AExample.txt"))
    
    [<Test>]
    member this.Day3B() =        
        Assert.AreEqual(877, day3B (readFileLines "./Day3Input.txt"))
