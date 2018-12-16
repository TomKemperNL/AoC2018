module Day9

open System
open System.Collections.Immutable

type Marble = Marble of int

type Circle = ImmutableList<Marble>
module Circle =    
    let private move circle start (modifier: int->int) : int= 
        let length = Seq.length circle
        let result = (modifier start) % length        
        if result >= 0 then result
        else 
            length + result

    let clockwise circle (start:int) (steps: int) : int =
        move circle start ((+) steps)
    let counterclockwise circle start steps : int =
        move circle start (fun x -> x - steps)

type GameSettings = {
    NrOfPlayers: int;
    LastMarble: int
}

type GameState = {    
    Circle: Circle
    CurrentIndex: int
}

type Outcome = Map<int, Marble list>

let step (game,outcome) ((Marble n),currentplayer) =
    if n % 23 =0 then
        let currentScore = Map.find currentplayer outcome
        let removePos = Circle.counterclockwise game.Circle game.CurrentIndex 7
        let extraMarble = game.Circle.Item removePos        
        let newOutcome = Map.add currentplayer (extraMarble :: Marble n :: currentScore) outcome        
        let newGame = {
            game with 
                Circle = game.Circle.RemoveAt removePos
                CurrentIndex = removePos
        }
        (newGame, newOutcome)
    else
        let insertPosition = Circle.clockwise game.Circle game.CurrentIndex 2
        let newCircle = game.Circle.Insert(insertPosition, Marble n)
        let newGame = { 
            game with 
                Circle = newCircle;
                CurrentIndex = insertPosition
        }
        (newGame, outcome)

let parse input = 
    match input with 
    | Regex "(\d+) players; last marble is worth (\d+) points" [players; points] -> 
        { NrOfPlayers = Int32.Parse players; LastMarble = Int32.Parse points; }
    | _ -> sprintf "Cant parse %s" input |> failwith 

let runGame marbles players =
    let infinitePlayers = seq { while true do yield! players }
    let turns = Seq.zip marbles infinitePlayers

    let initialOutcomes = players |> Seq.map (fun p -> (p, List.empty)) |> Map.ofSeq    
    let initialGame = {
        Circle = ImmutableList.Create(Marble 0);
        CurrentIndex = 0
    }
    
    let (_, finalOutcomes) = Seq.fold step (initialGame, initialOutcomes) turns

    let score (marbles: Marble seq) : int64 =
        marbles |> Seq.map (fun (Marble m) -> (int64)m) |> Seq.sum

    finalOutcomes |> Map.toSeq |> Seq.map snd |> Seq.map score |> Seq.sortDescending |> Seq.head

let day9 input =
    let settings = parse input
    let marbles = seq { 1 .. settings.LastMarble } |> Seq.map Marble
    let players = seq { 1 .. settings.NrOfPlayers }
    runGame marbles players

let day9B input = //Nope that is not what they meant:)
    let settings = parse input
    let marbles =  Seq.map Marble <| seq { 
        for i in 1 .. (settings.LastMarble - 1) do
            yield i;        
        yield settings.LastMarble * 100
    }
    let players = seq { 1 .. settings.NrOfPlayers }
    runGame marbles players   

