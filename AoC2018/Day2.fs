module Day2

open System

let hasNSameChars n (s: string) =    
    let counts = Seq.countBy id s
    Seq.exists (fun (ch,oc) -> oc = n) counts
    
let day2A (ss: string[]) =
    let exactlyTwoSames = Array.filter (hasNSameChars 2) ss |> Array.length
    let exactlyThreeSames = Array.filter (hasNSameChars 3) ss |> Array.length
    exactlyTwoSames * exactlyThreeSames

let compare (s1: string) (s2: string) =
    if s1.Length <> s2.Length then failwith "Strings of uneven length"
    let max = s1.Length - 1
    seq { for i in 0 .. max -> s1.Chars i = s2.Chars i }
 
let compareWithList (ss: string[]) (s: string) =
    let onlyOneDifference x = 
        let matches = compare x s 
        let nrOfMatches = Seq.filter id matches |> Seq.length
        if s.Length - nrOfMatches = 1 then
            let ix = Seq.findIndex not matches            
            Some (x.Remove(ix, 1))
        else None
    Array.choose onlyOneDifference ss

let day2B (ss: string[]) =
    let results = Array.map (compareWithList ss) ss |> (Array.filter (fun(x) -> x.Length > 0))
    let ouch = results.[0].[0]
    ouch

    
    

