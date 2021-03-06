﻿module TwoD

open System
open System.IO

[<Struct>]
type Coord = 
    | Coord of int*int

module Coord =
    let neighbors (Coord (x,y)) = 
        [ Coord (x - 1, y); Coord (x + 1, y); Coord (x, y - 1); Coord (x, y + 1) ]

    let distance (Coord (x1,y1)) (Coord (x2,y2)) =        
        Math.Abs(x1 - x2) + Math.Abs((y1 - y2))

type Bounds = {
    TopLeft: Coord
    BottomRight: Coord
}

module Bounds =
    let area { TopLeft=Coord (x1,y1); BottomRight=Coord (x2,y2)} = 
        (x2 - x1) * (y2 - y1)      
    
    let inBounds { TopLeft =  Coord(minX, minY); BottomRight = Coord(maxX, maxY)} (Coord(x,y)) =
        (x >= minX) && (x <= maxX) && (y >= minY) && ( y <= maxY )

    let outOfBounds b c = not <| inBounds b c
        
    let find (input: Coord seq) : Bounds =        
        let expand { TopLeft = Coord(minX,minY); BottomRight = Coord(maxX,maxY)} (Coord(x,y)) =
            {
                TopLeft = Coord(Math.Min(minX, x), Math.Min(minY, y))            
                BottomRight = Coord(Math.Max(maxX, x), Math.Max(maxY, y))        
            }        

        if Seq.length input = 0 then 
            { TopLeft = Coord(0, 0); BottomRight = Coord(0,0) }
        else
            let (Coord(x,y)) = Seq.head input
            let t = Seq.skip 1 input        
            input |> Seq.fold expand { TopLeft = Coord (x,y); BottomRight = Coord(x,y) }           

    let size { TopLeft= Coord(x1,y1); BottomRight= Coord(x2,y2)} =
        (Math.Abs(x1 - x2) + 1, Math.Abs(y1 - y2) + 1)
   
let render (writer: TextWriter) (tokens: Map<'T option, string>) bounds (points: Map<Coord, 'T>) : unit =
    let (width, height) = Bounds.size bounds
    let (Coord (tlx, tly)) = bounds.TopLeft
    let offSetX = -1 * tlx
    let offSetY = -1 * tly
    let array = Array2D.create width height (Map.find None tokens)
    let offSetPoints = Map.toSeq points |> Seq.map (fun (Coord (x,y), symbol) -> (Coord (x + offSetX, y + offSetY), symbol))
    let maxY = height - 1
    let maxX = width - 1

    for (Coord (x,y), symbol) in offSetPoints do
        if x >= 0 && x <= maxX && y >= 0 && y <= maxY then
            Array2D.set array x y (Map.find (Some symbol) tokens)
    for y in 0..maxY do
        for x in 0..maxX do
            writer.Write(Array2D.get array x y)
        writer.WriteLine()
    writer.Flush()
