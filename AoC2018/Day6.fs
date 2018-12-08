module Day6

open System

type Coord = (int*int)

module Coord =
    let Neighbours (x,y) = 
        [ (x - 1, y); (x + 1, y); (x, y - 1); (x, y + 1) ]

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
    
    let find inputs =
        let update { TopLeft=(mx,my); BottomRight=(Mx,My) } (x,y) = 
            { TopLeft = Math.Min(mx, x), Math.Min(my,y); BottomRight = Math.Max(Mx,x), Math.Max(My,y) }
        Seq.fold update { TopLeft = (0,0); BottomRight = (0,0) } inputs

type ClaimStatus = 
    | Tied
    | Claimed of Coord

type Claim = Coord * Coord list

let parse input =
    match input with 
    | Regex "(\d+), (\d+)" [x ; y] -> (Int32.Parse(x), Int32.Parse(y))    
    | _ -> failwith "parse-error"


let expandFront (claims: Claim seq) : Claim seq =
    let expand (c, points) = 
        (c, List.collect (Coord.Neighbours) points |> List.distinct)
    claims |> Seq.map expand
    
let claim map (newClaims: seq<Claim>) =       
    let buildLookup lookup ((owner, coords): Claim) =        
        let addCoord lookup coord =
            match Map.tryFind coord lookup with
            | Some owners -> Map.add coord (Set.add owner owners) lookup
            | None -> Map.add coord (set [owner]) lookup

        Seq.fold addCoord lookup coords        
    let lookup = Seq.fold buildLookup Map.empty newClaims

    let updateMap map coord (owners: Set<Coord>) = 
        match owners with         
        | s when (Set.count s = 0) -> failwith "Whut?"
        | s when (Set.count s = 1) -> Map.add coord (Claimed (Set.maxElement s)) map
        | _ -> Map.add coord Tied map

    Map.fold updateMap map lookup


let blobmap map (initialClaims: Claim seq) infinitepoints = 
    let bounds = Bounds.find (initialClaims |> Seq.collect snd)

    let rec blobRec map (claims: Claim seq) (infinitepoints: Set<Coord>) =    
        let checkBounds (goodClaims, infinites: Set<Coord>) (owner, coords) =
            let (outBounds, inBounds) = List.partition (Bounds.outOf bounds) coords
            let infinites2 = 
                if 
                    List.isEmpty outBounds then infinites 
                else 
                    let x = Set.add owner infinites
                    x
            ((owner, inBounds)::goodClaims, infinites2)
        
        let (goodClaims, infinites) = Seq.fold checkBounds ([], infinitepoints) claims
        
        if Seq.isEmpty (goodClaims |> Seq.collect snd) then
            (map, infinites)
        else
            let (map, untiedClaims) = claim map goodClaims

            let filterClaim (o, coords) =
                (o, coords |> List.filter (fun c -> not <| Map.containsKey c map))
            let front = expandFront untiedClaims |> Seq.map filterClaim

            blobRec map front infinites

    blobRec map initialClaims Set.empty

let day6A inputs =
    let inputs = inputs |> Seq.map parse     
    let initialMap = Map.empty
    let initialClaims = inputs |> Seq.map (fun c -> (c, [c]))

    let (map, infinitepoints) = blobmap initialMap initialClaims Set.empty
    let result = map |> Map.toSeq |> Seq.filter (fun (coord, claimStatus) ->
        match claimStatus with 
        | Tied -> false
        | Claimed coord -> not <| Set.contains coord infinitepoints ) |> Seq.countBy snd |> Seq.sortByDescending snd 

    let answer = result |> Seq.head |> snd
    answer

let day6B inputs = 
    null