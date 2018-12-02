module AoC2018.Tests.Day1

open System.IO;
open NUnit.Framework
open Day1
open System

[<TestFixture>]
type Day1Tests() =
    let path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./Day1Input.txt")

    [<TestCase(3, "+1, -2, +3, +1")>]
    [<TestCase(3, "+1, +1, +1")>]
    [<TestCase(0, "+1, +1, -2")>]
    [<TestCase(-6, "-1, -2, -3")>]
    member this.Day1AExamples(exp, input) =
       Assert.AreEqual(exp, (day1AExamples input))
    
    [<Test>]
    member this.Day1A() =       
       let input: string[] = File.ReadAllLines(path)
       Assert.AreEqual(484, (day1A input))
    
    [<TestCase(2, "+1, -2, +3, +1")>]
    [<TestCase(0, "+1, -1")>]
    [<TestCase(10, "+3, +3, +4, -2, -4")>]
    [<TestCase(5, "-6, +3, +8, +5, -6")>]
    [<TestCase(14, "+7, +7, -2, -7, -4")>]
    member this.Day1BExamples(exp, input) =
       Assert.AreEqual(exp, (day1BExamples input))    
       
    [<Test>]
    member this.Day1B() =       
       let input: string[] = File.ReadAllLines(path)
       Assert.AreEqual(367, (day1B input))