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
    
    let isCompleted progress action = 
        Seq.contains action progress.Completed 

    let isInProgress progress action : bool= 
        let workersWork worker = 
            match worker with
            | Idle -> None
            | WorkOn (a, t) -> Some a
        Seq.choose workersWork progress.Workers |> Seq.contains action
    
    let isAvailable rules (progress: Progress) action : bool =
        if isCompleted progress action then false        
        else if isInProgress progress action  then false
        else
        let unsatisfiedRules = Seq.filter (fun (c,q) -> not <| Seq.contains c progress.Completed) rules
        not <| Seq.exists (fun (c,q) -> q = action) unsatisfiedRules

    let duration (basecost: int) (action: Action) =
        basecost + (Map.find action actionsMap)
    
let parse (input: string) : Rule = 
    match input with 
    | Regex "Step ([A-Z]) must be finished before step ([A-Z]) can begin." [a; b] -> (a.Chars 0, b.Chars 0)
    | _ -> failwith "parse error"

let rec step scenario (progress: Progress) =     
    let advanceWork (workers, items) worker =
        match worker with 
        | Idle -> (worker::workers, items)
        | WorkOn (action, start) -> 
            let actionEnd = (scenario.CostFunction action) + start 
            if progress.Timer >= actionEnd then
                (Idle::workers, action::items)
            else
                (worker::workers, items)

    let (advancedWorkers, finishedItems) = Seq.fold advanceWork (List.empty, List.empty) progress.Workers

    let advancedProgress = {
        progress with 
            Completed = (List.append finishedItems progress.Completed);
            Workers = advancedWorkers
    }
    
    let availableWork = Seq.filter (Action.isAvailable scenario.Rules advancedProgress) scenario.Actions |> Seq.sort |> Seq.toList

    let assignWork ((workers: Worker list), (availableWork: Action list)) worker =
        match worker with 
        | WorkOn _ -> (worker::workers, availableWork)
        | Idle -> 
            match availableWork with 
            | [] -> (Idle :: workers, availableWork)
            | nextItem :: remainingWork -> ((WorkOn (nextItem,advancedProgress.Timer)) :: workers, remainingWork)
 
    let (assignedWorkers, _) = Seq.fold assignWork (List.empty, availableWork) advancedProgress.Workers

    let assignedProgress = {
        advancedProgress with          
            Workers = assignedWorkers
    }

    if (Seq.length scenario.Actions) = (List.length assignedProgress.Completed) then
        { assignedProgress with
           Completed = List.rev assignedProgress.Completed }
    else
        let progress = {
            assignedProgress with          
                Timer = assignedProgress.Timer + 1                
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