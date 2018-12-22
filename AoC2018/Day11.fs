module Day11

open System.Collections.Generic

type FuelCell = 
    | Cell of int*int

module FuelCell = 
    let power serial (Cell (x,y)) = 
        //Keep only the hundreds digit of the power level (so 12345 becomes 3; 
        let hundreds nr = ((nr - (nr % 100)) % 1000) / 100 
        
        let rackId = x + 10
        rackId 
            |> (*) y
            |> (+) serial
            |> (*) rackId
            |> hundreds 
            |> fun p -> p - 5  


let memoize fn =
    let cache = new Dictionary<_,_>()
    (fun x -> 
        match cache.TryGetValue x with
        | true, v -> 
            v
        | false, _ -> 
            let result = fn x
            cache.Add(x, result)
            result
    )

let squarePower powergrid = 
    let rec squarePowerInner (x,y, size) = 
        let extraPower col row =
            Seq.sum <| seq {
                for x in x..(col - 1) do
                    yield Array2D.get powergrid x row
                for y in y..(row - 1) do
                    yield Array2D.get powergrid col y
                yield Array2D.get powergrid col row
            }
        match size with 
        | 1 -> 
            Array2D.get powergrid x y
        | n -> 
            let extraColumn = x + size - 1
            let extraRow = y + size - 1

            let additional = (extraPower extraColumn extraRow) 
            let prev = (memoizedSquare (x,y, n - 1))
            additional + prev    
    and memoizedSquare : (int*int*int -> int) = memoize squarePowerInner
    memoizedSquare

let squarePowerOld (powergrid: int[,]) (topLeftX, topLeftY, (squareSize: int)) =
    Seq.sum <| seq {
        for x in topLeftX..(topLeftX + squareSize - 1) do
        for y in topLeftY..(topLeftY + squareSize - 1) do
            yield Array2D.get powergrid x y
    }

let createGrid max input =
    let cellPower = FuelCell.power input
    Array2D.initBased 1 1 max max (fun x y -> cellPower (Cell(x,y)))
    

let day11A input =     
    let max = 300
    let squareSize = 3
    let powergrid = createGrid max input
        
    let squares = seq {
        for x in 1 .. (max - squareSize + 1) do
        for y in 1 .. (max - squareSize + 1) do
        yield (x,y, squareSize)
    }

    let (x,y,_) = Seq.maxBy (squarePower powergrid) squares
    (x,y)

let day11B input =    
    let max = 300    
    let powergrid = createGrid max input

    let squares = seq {
        for w in 1 .. 300 do //the actual 300 takes 1 minute
        for x in 1 .. (max - w + 1) do
        for y in 1 .. (max - w + 1) do
        
        yield (x,y, w)
    }
    
    Seq.maxBy (squarePower powergrid) squares