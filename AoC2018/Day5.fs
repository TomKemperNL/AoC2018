module Day5

open System
open System.Security.Cryptography

let shouldReact (c1: char) (c2: char) : bool =
    (not (c1.ToString().Equals(c2.ToString()))) && c1.ToString().Equals(c2.ToString(), StringComparison.InvariantCultureIgnoreCase)

let charsToString cs =
    new String(cs |> List.toArray)

let rec fix f i =
    let result = f i
    printfn "%s" (charsToString result) |> ignore
    if result = i then result
    else fix f result

let day5A (input: string) :string=
    printfn "lalala"
    let rec react (polymer: char list) : char list=
        match polymer with
        | h1::t1 ->
            match t1 with 
            | h2::t2 ->
                if shouldReact h1 h2 then react t2
                else h1 :: react t1
            | [] -> h1 :: react t1
        | [] -> polymer

    let result = fix react (List.ofSeq input) |> List.toArray
    new String(result)


let day5B input = 
    null