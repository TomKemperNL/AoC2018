open Day1

// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

[<EntryPoint>]
let main argv = 
    let input = System.IO.File.ReadAllLines("./Day1Input.txt")
    let result = day1B input
    printfn "%d" result    
    0 // return an integer exit code
