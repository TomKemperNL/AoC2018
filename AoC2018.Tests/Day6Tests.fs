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
        let path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./Day6Input.txt")
        Assert.AreEqual(-1, (day6A (File.ReadAllLines(path))))

    [<Test>]
    member this.Day6B() =
        let path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./Day6Input.txt")
        let result = day6B (File.ReadAllLines(path))
        Assert.AreEqual(-1, result)

