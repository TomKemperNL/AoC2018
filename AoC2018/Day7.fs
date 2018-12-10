module Day7

open System.Linq
open System.Threading.Tasks

type Action = char

type Rule = Action*Action

type Worker = 
    | Idle
    | WorkOn of Action * int

type Scenario = {
    Rules: Rule seq
    Actions: Action seq    
    CostFunction: Action -> int
}

type Progress = {    
    Workers: Worker seq
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
    let advanceWork (workers, items) worker =
        match worker with 
        | Idle -> (worker::workers, items)
        | WorkOn (action, start) -> 
            if progress.Timer >= (scenario.CostFunction action) + start then
                (Idle::workers, action::items)
            else
                (worker::workers, items)

    let (updatedWorkers, finishedItems) = Seq.fold advanceWork (List.empty, List.empty) progress.Workers

    let progress = {
        progress with 
            Completed = (List.append finishedItems progress.Completed);
            Workers = updatedWorkers
    }
    
    let availableWork = Seq.filter (Action.isAvailable scenario.Rules progress.Completed) scenario.Actions |> Seq.sort |> Seq.toList

    let assignWork ((workers: Worker list), (availableWork: Action list)) worker =
        match worker with 
        | WorkOn _ -> (worker::workers, availableWork)
        | Idle -> 
            match availableWork with 
            | [] -> (Idle :: workers, availableWork)
            | nextItem :: remainingWork -> ((WorkOn (nextItem,progress.Timer)) :: workers, remainingWork)
 
    let (updatedWorkers, _) = Seq.fold assignWork (List.empty, availableWork) progress.Workers

    let progress = {
        progress with          
            Workers = updatedWorkers
    }

    if (Seq.length scenario.Actions) = (List.length progress.Completed) then
        { progress with
           Completed = List.rev progress.Completed }
    else
        let progress = {
            progress with          
                Timer = progress.Timer + 1                
        }

        step scenario progress
        

let day7A (inputs: string[]) =
    let rules = inputs |> Seq.map parse
    let allActions = rules |> Seq.collect (fun (c,r) -> [c; r]) |> Seq.distinct

    let costFunction act = 1

    let scenario = { Rules = rules; Actions= allActions; CostFunction = costFunction} 
    let progress = { Completed = List.empty; Timer = 0; Workers= [Idle];}
    
    let result = step scenario progress
    new string(result.Completed |> List.toArray)
    
let day7B workerCount baseCost inputs = 
    let costFunction = Action.duration baseCost
    let rules = inputs |> Seq.map parse
    let allActions = rules |> Seq.collect (fun (c,r) -> [c; r]) |> Seq.distinct
    let workers = Seq.init workerCount (fun _ -> Idle)
    let scenario = { Rules = rules; Actions= allActions; CostFunction = costFunction} 
    let progress = { Completed = List.empty; Timer = 0; Workers= workers;}

    let result = step scenario progress
    result.Timer