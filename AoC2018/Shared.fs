[<AutoOpen>]
module Shared

open System.Text.RegularExpressions

let (|Regex|_|) pattern input = 
    let m = Regex.Match(input, pattern)
    if m.Success then 
        let max = m.Groups.Count - 1
        let values = seq {
            for i in 1 .. max do
                yield m.Groups.[i].Value
        }
        let result = values |> Seq.toList 
        Some (result)
    else None
