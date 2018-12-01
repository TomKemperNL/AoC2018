module Day1

open System
open System.Collections.Generic
open System.Collections

type Bits() =
    let increment =  8 * 1024 * 100
    let mutable size = 8 * 1024 * 100
    let mutable pbits = new BitArray(size)
    let mutable nbits = new BitArray(size)

    let resize (nr: int) =
        let anr = Math.Abs(nr)
        if anr >= size then         
            size <- anr + increment
            pbits.Length <- size
            nbits.Length <- size

    member this.getBit (nr:int) =
        resize nr

        if nr < 0 then nbits.[Math.Abs(nr)] else pbits.[nr]    
    member this.setBit (nr:int) =
        resize nr

        if nr < 0 then nbits.[Math.Abs(nr)] <- true else pbits.[nr] <- true    
    member this.unsetBit (nr:int) =
        resize nr

        if nr < 0 then nbits.[Math.Abs(nr)] <- false else pbits.[nr] <- false    
            

let parseToken (s:String) =    
    match s with
    | null -> failwith "Cannot parse null"
    | "0" -> id
    | s when s.Length < 2 -> failwith (sprintf "Cannot parse %s" s)
    | s ->
        let token = s.Chars 0
        let success, value = Int32.TryParse (s.Substring 1)
        match success, token, value with
        | false, _, _ -> failwith (sprintf "Cannot parse %s" s)
        | true, '+', x -> 
            fun(a) -> a + x
        | true, '-', x -> 
            fun(a) -> a - x
        | true, _, _ -> failwith (sprintf "Cannot parse %s" s)

let day1A (inputs: String[]) = 
        inputs
        |> Array.map (fun s -> s.Trim())
        |> Array.map parseToken 
        |> Array.fold (|>) 0 

let processInput (input, alreadyFoundIt, collector: Bits) (operator: int->int) =
    let result = operator input
    match alreadyFoundIt with
    | Some x -> 
        (result, Some x, collector)
    | None ->
        let foundIt = 
            if collector.getBit result
            then 
                Some(result) 
            else
                collector.setBit result
                None
        (result, foundIt, collector)

let day1B (inputs: String[]) =    
    let ops = 
        inputs
            |> Array.map (fun s -> s.Trim())
            |> Array.map parseToken
    
    let numbers = new Bits()
    numbers.setBit 0

    let rec findit state =
        let (newint, foundIt, foundNumbers) = Array.fold processInput state ops
        match foundIt with
        | None -> 
            findit (newint, foundIt, foundNumbers)
        | Some x -> 
            x

    findit (0, None, numbers)

let day1AExamples (s: string) =
    s.Split(',') |> day1A

let day1BExamples (s: string) =
    s.Split(',') |> day1B

