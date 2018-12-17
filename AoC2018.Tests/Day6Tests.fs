module Day6Tests

open NUnit.Framework
open System.IO
open System
open Day6


[<TestFixture>]
type Day6Tests() =

    [<Test>]
    member this.Day6AExample() =       
        let input : string[] = [| "1, 1";"1, 6";"8, 3";"3, 4";"5, 5";"8, 9" |]
        Assert.AreEqual(17, day6A input)

    [<Test>]
    member this.Day6A() =        
        Assert.AreEqual(3871, (day6A (readFileLines "./Day6Input.txt")))
   
   
    [<Test>]
    member this.Day6BExample() =
        let input : string[] = [| "1, 1";"1, 6";"8, 3";"3, 4";"5, 5";"8, 9" |]
        let result = day6B 32 input
        Assert.AreEqual(16, result)


    [<Test>]
    member this.Day6B() =        
        Assert.AreEqual(44667, (day6B 10000 (readFileLines "./Day6Input.txt")))

