module Day12Tests

open Day12
open NUnit.Framework

[<TestFixture>]
type Day12Tests() =
    [<Test>]
    member this.Day12AExample() =
        Assert.AreEqual(325, (day12A (readFileLines "Day12Example.txt")))

    [<Test>]
    member this.Day12A() =
        Assert.AreEqual(0, (day12A (readFileLines "Day12Input.txt")))
    