module Day2

let hasNSameChars n (s: string) =    
    let counts = Seq.countBy id s
    Seq.exists (fun (ch,oc) -> oc = n) counts
    
let day2A (ss: string[]) =
    let exactlyTwoSames = Array.filter (hasNSameChars 2) ss |> Array.length
    let exactlyThreeSames = Array.filter (hasNSameChars 3) ss |> Array.length
    exactlyTwoSames * exactlyThreeSames


let day2B (ss: string[]) =
    "nope"