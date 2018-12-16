module Day8Tests


open NUnit.Framework
open System.IO
open System
open Day8

[<TestFixture>]
type Day8Tests() =

    [<Test>]
    member this.TestParse() =
        let expected = {
            Header = Header(1,2)
            Metadata = [Metadata 2; Metadata 5]
            Children = [{
                Header = Header(0,1)
                Metadata = [Metadata 7]
                Children = []
            }]
        }
        let input = "1 2 0 1 7 2 5"

        Assert.AreEqual (expected, (parse input))


    [<Test>]
    member this.ExampleA() =
        let input = "2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2"
        Assert.AreEqual (138, (day8A input))

    [<Test>]
    member this.Day8A() =
        let input = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./Day8Input.txt"))
        Assert.AreEqual (43351, (day8A input))

    [<Test>]
    member this.ExampleB() =
        let input = "2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2"
        Assert.AreEqual (66, (day8B input))

    
    [<Test>]
    member this.Day8B() =
        let input = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./Day8Input.txt"))
        Assert.AreEqual (21502, (day8B input))