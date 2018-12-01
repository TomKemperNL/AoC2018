module Day1

open System
open System.Collections.Generic

let parseToken (s:String) =    
    match s with
    | null -> id
    | "0" -> id
    | s when s.Length < 2 -> id
    | s ->
        let token = s.Chars 0
        let success, value = Int32.TryParse (s.Substring 1)
        match success, token, value with
        | false, _, _ -> id
        | true, '+', x -> 
            fun(a) -> a + x
        | true, '-', x -> 
            fun(a) -> a - x
        | true, _, _ -> id

let day1A (inputs: String[]) = 
        inputs
        |> Array.map (fun s -> s.Trim())
        |> Array.map parseToken 
        |> Array.fold (|>) 0 

let processInput (collector: List<int>) (input, alreadyFoundIt) (operator: int->int) =
    let result = operator input
    match alreadyFoundIt with
    | Some x -> 
        (result, Some x)
    | None ->
        let foundIt = 
            if collector.Contains(result) 
            then Some(result) 
            else
                collector.Add(result)
                None
        (result, foundIt)

let day1B (inputs: String[]) =    
    let seenValues = new List<int>()
    seenValues.Add(0)

    let mutable current: int = 0
    let mutable result: int option = None

    while result = None do
        let (newint, foundIt) = 
            inputs
                |> Array.map (fun s -> s.Trim())
                |> Array.map parseToken 
                |> Array.fold (processInput seenValues) (current, result) 
        current <- newint
        result <- foundIt
    result.Value

let day1AExamples (s: string) =
    s.Split(',') |> day1A

let day1BExamples (s: string) =
    s.Split(',') |> day1B

