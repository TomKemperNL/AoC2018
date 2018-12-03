module Day3

open System
open System.Text.RegularExpressions
open System.Collections.Generic

type Size =
    | Size of int*int

[<Struct>]
type Coord = 
    | Coord of int*int

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

let addToIndex (index: Index) claim =
    match claim with
    | Claim (_, Coord(x,y), Size(w,h)) ->        
        for ix in x .. (x + w - 1) do
        for iy in y .. (y + h - 1) do
        let coord = Coord(ix,iy)
        if index.ContainsKey(coord) then
            index.[coord].Add(claim) |> ignore
        else
            let freshList : HashSet<Claim> = new HashSet<Claim>()
            freshList.Add(claim) |> ignore
            index.Add(coord, freshList) 
        

let createIndex claims =
    let index = new Index()
    for c in claims do
        addToIndex index c |> ignore
    index

let day3A (input: string[]) =
    let parsed = Array.map parse input
    let index = createIndex parsed    
    index.Values |> Seq.filter (fun(kv: HashSet<Claim>) -> kv.Count > 1) |> Seq.length

let findPartners (index: Index) (claim: Claim) : HashSet<Claim> =
    let partners = new HashSet<Claim>()        
    match claim with
    | Claim (_, Coord(x,y), Size(w,h)) ->    
        for ix in x .. (x + w - 1) do
        for iy in y .. (y + h - 1) do        
        let coord = Coord (ix, iy)
        for p in index.[coord] do
        partners.Add p |> ignore    
    partners

let day3B (input: string[]) =
    let parsed = Array.map parse input
    let index = createIndex parsed 
    let solo = Array.find (fun(c) -> findPartners index c |> Seq.length = 1) parsed    
    match solo with 
    | Claim (id,_,_) -> id

    


    
