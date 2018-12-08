module Day6

open System

type Coord = (int*int)

module Coord =
    let Neighbours (x,y) = 
        [| (x - 1, y); (x + 1, y); (x, y - 1); (x, y + 1) |]

type Kind = 
    | Infinite
    | Finite

type Bounds = {
    TopLeft: Coord
    BottomRight: Coord
}

module Bounds =
    let size { TopLeft=(x1,y1); BottomRight=(x2,y2)} = 
        (x2 - x1) * (y2 - y1)
    
    let outOf { TopLeft=(x1,y1); BottomRight=(x2,y2)} (x,y)  =
        x < x1 || x > x2 || y < y1 || y > y2

type Claim = 
    | Tied
    | Claimed of Coord

let parse input =
    match input with 
    | Regex "(\d+), (\d+)" [x ; y] -> (Int32.Parse(x), Int32.Parse(y))    
    | _ -> failwith "parse-error"

let findBounds inputs =
    let update { TopLeft=(mx,my); BottomRight=(Mx,My) } (x,y) = 
        { TopLeft = Math.Min(mx, x), Math.Min(my,y); BottomRight = Math.Max(Mx,x), Math.Max(My,y) }
    Seq.fold update { TopLeft = (0,0); BottomRight = (0,0) } inputs

let rec claim map todo n bounds = 
    let update map ((x,y), claims) =
        match Map.tryFind (x,y) map with 
        | Some x -> map
        | None -> 
            match claims with 
            | cs when Seq.length cs = 0 -> Map.add (x,y) (Claimed (x,y)) map
            | cs when Seq.length cs = 1 -> Map.add (x,y) (Seq.exactlyOne cs) map
            | _ -> Map.add (x,y) Tied map            
    
    let newPoints = 
        Map.toSeq map |> Seq.map fst |> Seq.collect Coord.Neighbours |> Seq.distinct |> Seq.filter (not << Bounds.outOf bounds)

    let pointsWithNeighbours = 
        newPoints |> Seq.map (fun p -> (p, Coord.Neighbours p |> Seq.choose (fun k -> Map.tryFind k map) |> Seq.distinct))
    
    let map = Seq.fold update map pointsWithNeighbours
    if Map.count map >= todo then map else claim map todo (n+1) bounds

let day6A inputs =
    let inputs = inputs |> Seq.map parse 
    
    let initialMap = inputs |> Seq.map (fun x -> (x, Claimed x)) |> Map.ofSeq
    let bounds = findBounds inputs

    let map = claim initialMap (Bounds.size bounds) 0 bounds
    let largest = Map.toSeq map |> Seq.countBy snd |> Seq.sortByDescending snd |> Seq.head |> snd
    largest


let day6B inputs = 
    null