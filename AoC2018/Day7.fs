module Day7

open System.Linq

type Action = char
type Rule = Action*Action

module Action =
    let isAvailable rules actionsPerformed action : bool =
        if Seq.contains action actionsPerformed then false
        else
        let unsatisfiedRules = Seq.filter (fun (c,q) -> not <| Seq.contains c actionsPerformed) rules
        not <| Seq.exists (fun (c,q) -> q = action) unsatisfiedRules
    
let parse (input: string) : Rule = 
    match input with 
    | Regex "Step ([A-Z]) must be finished before step ([A-Z]) can begin." [a; b] -> (a.Chars 0, b.Chars 0)
    | _ -> failwith "parse error"

let rec step rules allActions actionsSoFar =    
    let next = Seq.filter (Action.isAvailable rules actionsSoFar) allActions |> Seq.sort
    if Seq.isEmpty next then
        List.rev actionsSoFar
    else
        step rules allActions ((Seq.head next) :: actionsSoFar)

let day7A (inputs: string[]) =
    let rules = inputs |> Seq.map parse
    let allActions = rules |> Seq.collect (fun (c,r) -> [c; r]) |> Seq.distinct
    let result: Action list = step rules allActions List.empty
    new string(result |> List.toArray)

    



let day7B inputs = 
    null