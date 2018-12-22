module Day11

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
      

let day11A input = 
    let cellPower = FuelCell.power input
    let max = 300
    let squareSize = 3

    let powergrid = Array2D.initBased 1 1 max max (fun x y -> cellPower (Cell(x,y)))

    let squarePower (topLeftX, topLeftY) =
        Seq.sum <| seq {
            for x in topLeftX..(topLeftX + squareSize - 1) do
            for y in topLeftY..(topLeftY + squareSize - 1) do
                yield Array2D.get powergrid x y
        } 
        
    let squares = seq {
        for x in 1 .. (max - squareSize + 1) do
        for y in 1 .. (max - squareSize + 1) do
        yield (x,y)
    }

    Seq.maxBy squarePower squares
