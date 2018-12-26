module Day3

open System
open System.Text.RegularExpressions
open System.Collections.Generic
open TwoD

type Size =
    | Size of int*int

type Claim = 
    | Claim of int*Coord*Size

type Index =
    Dictionary<Coord, HashSet<Claim>>    

let parse (s:string) : Claim =
    let regMatch = Regex.Match(s, "\#(\d+) \@ (\d+),(\d+)\: (\d+)x(\d+)")
    if regMatch.Success then
        let toDigit (n: int) : int =
            regMatch.Groups.[n].Value |> Int32.Parse
        Claim (toDigit 1, Coord(toDigit 2, toDigit 3), Size(toDigit 4, toDigit 5))
    else failwith(sprintf "Could not parse %s" s)

let coords claim = 
    match claim with
    | Claim (_, Coord(x,y), Size(w,h)) ->       
        seq {
            for ix in x .. (x + w - 1) do
            for iy in y .. (y + h - 1) do
            yield Coord(ix,iy)
        }

let createIndex claims =    
    let addToIndex (index: Index) (claim: Claim) :Index =
        let add (coord: Coord) = 
            if index.ContainsKey(coord) then
                index.[coord].Add(claim) |> ignore
            else
                let freshList : HashSet<Claim> = new HashSet<Claim>()
                freshList.Add(claim) |> ignore
                index.Add(coord, freshList)
        coords claim |> Seq.iter add
        index

    Seq.fold addToIndex (new Index()) claims    

let day3A (input: string[]) =
    let parsed = Array.map parse input
    let index = createIndex parsed
    index.Values |> Seq.filter (fun(kv: HashSet<Claim>) -> kv.Count > 1) |> Seq.length
    
let day3B (input: string[]) =
    let parsed = Array.map parse input
    let index = createIndex parsed 

    let findPartners (index: Index) (claim: Claim) : Set<Claim> =        
        let addPartners (partners: Set<Claim>) (coord: Coord) : Set<Claim>=
            Seq.fold (fun x y -> Set.add y x) partners index.[coord]            
        coords claim |> Seq.fold addPartners Set.empty        

    let solo = Array.find (fun(c) -> findPartners index c |> Seq.length = 1) parsed    
    match solo with 
    | Claim (id,_,_) -> id

    


    
