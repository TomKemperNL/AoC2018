module Day11Tests

open NUnit.Framework
open Day11

[<TestFixture>]
type Day11Tests() =

    [<Test>]
    member this.Day11PowerExamples() =
        Assert.AreEqual(4, FuelCell.power 8 (Cell (3,5)))
        Assert.AreEqual(-5, FuelCell.power 57 (Cell (122,79)))
        Assert.AreEqual(0, FuelCell.power 39 (Cell (217,196)))
        Assert.AreEqual(4, FuelCell.power 71 (Cell (101,153)))

    [<Test>]
    member this.Day11SquareExamples() =
        Assert.AreEqual((33,45), day11A 18)
        Assert.AreEqual((21,61), day11A 42)
    
    [<Test>]
    member this.Day11A() =
        Assert.AreEqual((20,77), day11A 9221)
        
    [<Test>]
    member this.Day11BSquareExamples() =
        Assert.AreEqual((90,269,16), day11B 18)
        Assert.AreEqual((232,251,12), day11B 42)

    [<Test>]
    member this.Day11B() =        
        Assert.AreEqual((143,57,10), day11B 9221)