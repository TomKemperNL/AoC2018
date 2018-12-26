open System
open Day10

// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

[<EntryPoint>]
let main argv = 
    let fileName = argv.[0]
    let inputs = System.IO.File.ReadLines(fileName)

    day10A Console.Out visualization inputs (fun() -> 
        Console.ReadKey() |> ignore
        Console.Clear()
    )    
    0 // return an integer exit code
