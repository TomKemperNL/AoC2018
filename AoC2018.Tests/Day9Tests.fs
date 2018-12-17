module Day9Tests

open NUnit.Framework
open Day9

[<TestFixture>]
type Day9Tests() =
    
    [<Test>]
    member this.RoundAndRound() =
        let circle = [ 0; 1; 2; 3; 4; 5 ]
        
        Assert.AreEqual (5, (Circle.clockwise circle 3 2), "Can go clockwise")
        Assert.AreEqual (1, (Circle.counterclockwise circle 3 2), "Can go counterclockwise")

        Assert.AreEqual (2, (Circle.clockwise circle 3 5), "Can go round clockwise")
        Assert.AreEqual (4, (Circle.counterclockwise circle 3 5), "Can go round counterclockwise")
        
        Assert.AreEqual (2, (Circle.clockwise circle 3 11), "Can go round clockwise twice")
        Assert.AreEqual (4, (Circle.counterclockwise circle 3 11), "Can go round counterclockwise twice")

    [<Test>]
    member this.Day9Examples() =
        let examples = [|
            ("9 players; last marble is worth 25 points",    32);
            ("10 players; last marble is worth 1618 points", 8317);
            ("13 players; last marble is worth 7999 points", 146373);
            ("17 players; last marble is worth 1104 points", 2764);
            ("21 players; last marble is worth 6111 points", 54718);
            ("30 players; last marble is worth 5807 points", 37305);
        |]
        for (input, expected) in examples do
            Assert.AreEqual(expected, (day9 input), input)

    [<Test>]
    member this.Day9A() = 
        let input = "441 players; last marble is worth 71032 points"
        Assert.AreEqual(393229, (day9 input))

    [<Test>]
    member this.Day9B() = 
        let input = "441 players; last marble is worth 7103200 points"        
        Assert.AreEqual(3273405195L, (day9 input))