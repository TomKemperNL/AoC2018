module AoC2018.Tests.Day3

open NUnit.Framework
open System.IO
open System
open Day3

[<TestFixture>]
type Day3Tests() =

    [<Test>]
    member this.Day3Parse() = 
        let expected = Claim (12,Coord (2,4), Size (5,6))
        Assert.AreEqual(expected, parse "#12 @ 2,4: 5x6")
       
    [<Test>]
    member this.Day3AExample() =
        let path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./Day3AExample.txt")
        Assert.AreEqual(4, day3A (File.ReadAllLines(path)))
    
    [<Test>]
    member this.Day3A() =
        let path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./Day3Input.txt")
        Assert.AreEqual(114946, day3A (File.ReadAllLines(path)))

    
    [<Test>]
    member this.Day3BExample() =
        let path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./Day3AExample.txt")
        Assert.AreEqual(3, day3B (File.ReadAllLines(path)))
    
    [<Test>]
    member this.Day3B() =
        let path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./Day3Input.txt")
        Assert.AreEqual(877, day3B (File.ReadAllLines(path)))
