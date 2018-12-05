


module Day4

open System
open System.Globalization
open System.Text.RegularExpressions
open System.Collections.Generic

[<Struct>]
type GuardAction = 
    | BeginShift of int
    | WakeUp
    | FallAsleep

[<Struct>]
type GuardState =
    | Awake
    | Asleep
    | Unknown


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

type Line = DateTime * GuardAction

let parse (line: string) : Line =    
    let timePart = line.Substring(0, 18)
    let actionPart = line.Substring(19)
    let dt = DateTime.ParseExact(timePart, "[yyyy-MM-dd HH:mm]", CultureInfo.InvariantCulture)
    match actionPart with 
    | Regex "Guard #(\d+) begins shift" [ id ] -> (dt, BeginShift (Int32.Parse id))
    | Regex "wakes up" [] -> (dt, WakeUp)
    | Regex "falls asleep" [] -> (dt, FallAsleep)
    | _ -> failwith (sprintf "unable to parse %s" line)

type GuardStats = int[]

type GuardReport = Dictionary<int, GuardStats>

type CurrentGuard = {
    id: int;
    state: GuardState
    since: DateTime
}

let totalSleep (stats: GuardStats) =
    Array.sum stats

let mostSleepyMinute (stats: GuardStats) =
    stats
        |> Array.mapi (fun (ix: int) (sleep: int) -> (ix, sleep)) 
        |> Array.maxBy snd


let addSleep (report: GuardReport) id (fromTime: DateTime) (toTime: DateTime) =
    if not (report.ContainsKey(id)) then
        report.Add(id, Array.zeroCreate 60)
    
    for i in fromTime.Minute .. (toTime.Minute - 1) do
        report.[id].[i] <- report.[id].[i] + 1
   

let createReport input =
    let lines = Array.map parse input
    let timeline = lines |> Array.sortBy (fun (dt,act) -> dt)

    let guardReport = new GuardReport()
    let mutable currentGuard : CurrentGuard = { id=0; state=Unknown; since=DateTime.MinValue }

    let gatherer ((dt, action) : Line)=
        match action with
        | (BeginShift id) -> 
            currentGuard <- { id= id; state=Awake; since=dt }
        | (WakeUp) ->            
            addSleep guardReport currentGuard.id currentGuard.since dt |> ignore
            currentGuard <- { currentGuard with state = Awake; since=dt; }
        | (FallAsleep) ->
            currentGuard <- { currentGuard with state = Asleep; since=dt; }

    Array.iter gatherer timeline
    guardReport

let day4A (input: string[]) =
    let guardReport = createReport input
    let sleepyGuard = Seq.maxBy (fun (kv:KeyValuePair<int,GuardStats>) -> totalSleep kv.Value) guardReport
    let mostSleepyMinute = mostSleepyMinute sleepyGuard.Value |> fst
    sleepyGuard.Key * mostSleepyMinute

let day4B (input: string[]) =
    let guardReport = createReport input
    let sleepyGuard = Seq.maxBy (fun (kv:KeyValuePair<int,GuardStats>) -> mostSleepyMinute kv.Value |> snd) guardReport
    let mostSleepyMinute = mostSleepyMinute sleepyGuard.Value |> fst
    sleepyGuard.Key * mostSleepyMinute