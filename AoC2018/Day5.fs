module Day5

open System
open System.Security.Cryptography

let shouldReact (c1: char) (c2: char) : bool =
    (not (c1.ToString().Equals(c2.ToString()))) && c1.ToString().Equals(c2.ToString(), StringComparison.InvariantCultureIgnoreCase)

let charsToString cs =
    new String(cs |> List.toArray)

let rec fix f i =
    let result = f i    
    if result = i then result
    else fix f result

let variant (polymer: char list) (utype: char) : char list =
    polymer |> List.filter (fun c -> not (c.ToString().Equals(utype.ToString(), StringComparison.InvariantCultureIgnoreCase)))

let react polymer =
    let rec reactRec acc (polymer: char list) :char list =
        match polymer with
        | h1::t1 ->
            match t1 with 
            | h2::t2 ->
                if shouldReact h1 h2 then reactRec acc t2
                else reactRec (h1 :: acc) t1
            | [] -> reactRec (h1:: acc) t1 
        | [] -> List.rev acc
    fix (reactRec [])  polymer

let day5A (input: string) :int =    
    react (List.ofSeq input) |> List.length
    


let day5B (input: string) :int= 
    let polymer = List.ofSeq input
    seq {
        for c in 'a' .. 'z' -> c
    } |> Seq.map (variant polymer) |> Seq.map react |> Seq.map Seq.length |> Seq.min