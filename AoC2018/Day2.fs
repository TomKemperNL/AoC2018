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
    seq { for i in 0 .. max -> s1.Chars i = s2.Chars i } |> Seq.toArray
 
let compareWithList (ss: string[]) (s: string) =
    let findSingleDifference x = 
        let matches: bool[] = compare x s        
        
        let countIf c b = 
            if not b then c + 1 else c
        let nrOfMismatches: int = Seq.fold countIf 0 matches 
        
        if nrOfMismatches = 1 then
            let ix = Seq.findIndex not matches            
            let withoutMismatch = (x.Remove(ix, 1))
            Some withoutMismatch
        else 
            None
    Array.tryPick findSingleDifference ss 

let day2B (ss: string[]) =
    Array.pick (compareWithList ss) ss  
    

    
    

