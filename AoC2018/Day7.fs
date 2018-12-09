module Day7

open System.Linq
open System.Threading.Tasks

type Action = char

type Rule = Action*Action

type Worker = 
    | Idle
    | WorkOn of Action

type Scenario = {
    Rules: Rule seq
    Actions: Action seq
    Workers: Worker seq
}

type Progress = {
    Completed: Action list
    Timer: int
}

module Action =
    let actionsMap :Map<Action,int> = 
        let letters = seq { 'A'..'Z' }
        let numbers = seq { 1 .. 26 }
        Seq.zip letters numbers |> Map.ofSeq

    let isAvailable rules actionsPerformed action : bool =
        if Seq.contains action actionsPerformed then false
        else
        let unsatisfiedRules = Seq.filter (fun (c,q) -> not <| Seq.contains c actionsPerformed) rules
        not <| Seq.exists (fun (c,q) -> q = action) unsatisfiedRules

    let duration (basecost: int) (action: Action) =
        60 + (Map.find action actionsMap)
    
let parse (input: string) : Rule = 
    match input with 
    | Regex "Step ([A-Z]) must be finished before step ([A-Z]) can begin." [a; b] -> (a.Chars 0, b.Chars 0)
    | _ -> failwith "parse error"

let rec step scenario (progress: Progress) =    
    let next = Seq.filter (Action.isAvailable scenario.Rules progress.Completed) scenario.Actions |> Seq.sort
    if Seq.isEmpty next then
        { progress with Completed = (List.rev progress.Completed) }
    else
        step scenario { progress with Completed = ((Seq.head next) :: progress.Completed); Timer = progress.Timer + 1 }

let day7A (inputs: string[]) =
    let rules = inputs |> Seq.map parse
    let allActions = rules |> Seq.collect (fun (c,r) -> [c; r]) |> Seq.distinct

    let result = step { Rules = rules; Actions= allActions; Workers= [Idle]} { Completed = List.empty; Timer = 0; }
    new string(result.Completed |> List.toArray)
    
let day7B workerCount baseCost inputs = 
    let actionCost = Action.duration baseCost    
    let workers = Seq.init workerCount (fun _ -> Idle)

    null