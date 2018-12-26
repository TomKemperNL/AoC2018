module Day12Tests

open Day12
open NUnit.Framework

[<TestFixture>]
type Day12Tests() =
    [<Test>]
    member this.Day12AExample() =
        Assert.AreEqual(325, (day12 (readFileLines "Day12Example.txt") 20L))

    [<Test>]
    member this.Day12A() =
        Assert.AreEqual(1991, (day12 (readFileLines "Day12Input.txt") 20L))

    [<Test>]
    member this.Day12B() =
        Assert.AreEqual(0, (day12 (readFileLines "Day12Input.txt") 50000000000L))
    