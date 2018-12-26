module Day6

open System
open TwoD

type Kind = 
    | Infinite
    | Finite

type ClaimStatus = 
    | Tied
    | Claimed of Coord

type Claim = Coord * Coord list

module Claim =    
    let filter f (o, coords) =
                (o, coords |> List.filter f)

let parse input =
    match input with 
    | Regex "(\d+), (\d+)" [x ; y] -> Coord (Int32.Parse(x), Int32.Parse(y))    
    | _ -> failwith "parse-error"


let expandFront (claims: Claim seq) : Claim seq =
    let expand (c, points) = 
        (c, List.collect (Coord.neighbors) points |> List.distinct)
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

    let newMap = Map.fold updateMap map lookup
    let untiedClaims = Seq.map (Claim.filter (fun coord -> (Map.find coord newMap) <> Tied)) newClaims
    (newMap, untiedClaims)

let blobmap map (initialClaims: Claim seq) infinitepoints = 
    let bounds = Bounds.find (initialClaims |> Seq.collect snd)

    let rec blobRec map (claims: Claim seq) (infinitepoints: Set<Coord>) =    
        let checkBounds (goodClaims, infinites: Set<Coord>) (owner, coords) =
            let (outBounds, inBounds) = List.partition (Bounds.outOfBounds bounds) coords
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
            let front = expandFront untiedClaims |> Seq.map (Claim.filter (fun c -> not <| Map.containsKey c map))

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

let day6B tolerance inputs = 
    let inputs = inputs |> Seq.map parse
    let bounds = Bounds.find inputs
    let (Coord (tlx, tly)) = bounds.TopLeft
    let (Coord (brx, bry)) = bounds.BottomRight
    seq {
        for x in tlx .. brx do
        for y in tly .. bry do

        let total = inputs |> Seq.map (fun i -> Coord.distance (Coord (x,y)) i) |> Seq.sum
        if total < tolerance then
            yield (x,y)

    } |> Seq.length
