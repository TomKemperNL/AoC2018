module Day12
open System.Collections


type Pot = 
    | Pot of bool
type State = Map<int,Pot>
type Rule = { Condition: bool*bool*bool*bool*bool; Result: bool }

type Input = {
    InitialState: State;
    Rules: Rule list
}

let parse (input: string array) = 
    let parseState (s: string) :State= 
        match s with 
        | Regex "initial state: ([\.\#]+)" [r] ->            
            r |> Seq.mapi (fun i c -> 
                if c = '#' then (i, Pot true) else (i, Pot false)) |> Map.ofSeq
        | _ -> failwith <| sprintf "Error parsing %s" s
    
    let parseRule (s: string) = 
        match s with
        | Regex "([\.\#]+) => ([\.\#])" [cond; result] ->            
            let isPot c = 
                c = '#'

            let condition = Array.map isPot (cond.ToCharArray())
            let condition = condition |> (fun c -> match c with 
                                                    | [|a;b;c;d;e|] -> (a,b,c,d,e)
                                                    | _ -> failwith <| sprintf "Wut! %A" c)
            { Condition =  condition; Result = isPot (result.Chars 0)}
        | _ -> failwith <| sprintf "Error parsing %s" s

    {
        InitialState = parseState input.[0];
        Rules = Array.skip 2 input |> Seq.map parseRule |> List.ofSeq
    }



let spread (rules: Rule list) (state: State) :State=    
    let pick n = 
        match (Map.tryFind n state) with
        | Some (Pot true) -> true
        | Some (Pot false) -> false
        | None -> false

    let shouldSet index =
        let slice = (pick (index - 2), pick (index - 1), pick index, pick (index + 1), pick (index + 2))
        let matchingRule = List.tryFind (fun r -> r.Condition = slice) rules
        match matchingRule with 
        | None -> false
        | Some x -> x.Result        
    
    let minIndex = Map.toSeq state |> Seq.map fst |> Seq.min
    let maxIndex = Map.toSeq state |> Seq.map fst |> Seq.max

    let newMap = seq { (minIndex - 2) .. (maxIndex + 2) } |> Seq.map (fun i -> (i, Pot <| shouldSet i)) |> Map.ofSeq
    newMap

let rec spreadN rules state n =
    match n with 
    | 0 -> state
    | n -> 
        let next = spread rules state
        spreadN rules next (n - 1)

let day12A input= 
    let input = parse input
    let state = spreadN input.Rules input.InitialState 20
    Map.toSeq state |> Seq.filter (fun (i, Pot b) -> b) |> Seq.map fst |> Seq.sum