module AoC2018.Tests.Day1

open NUnit.Framework.Internal
open NUnit.Framework
open Day1

[<TestFixture>]
type Class1() =     
    
    [<TestCase(3, "+1, -2, +3, +1")>]
    [<TestCase(3, "+1, +1, +1")>]
    [<TestCase(0, "+1, +1, -2")>]
    [<TestCase(-6, "-1, -2, -3")>]
    member this.A(exp, input) =
       Assert.AreEqual(exp, (day1AExamples input))

    
    [<TestCase(2, "+1, -2, +3, +1")>]
    [<TestCase(0, "+1, -1")>]
    [<TestCase(10, "+3, +3, +4, -2, -4")>]
    [<TestCase(5, "-6, +3, +8, +5, -6")>]
    [<TestCase(14, "+7, +7, -2, -7, -4")>]
    member this.B(exp, input) =
       Assert.AreEqual(exp, (day1BExamples input))    