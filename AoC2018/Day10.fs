module Day10

open System
open System.IO

type Coord = { X: int; Y: int;}
type Bounds = { TopLeft: Coord; BottomRight: Coord}
module Bounds =
    let size { TopLeft= { X =x1; Y=y1 }; BottomRight= { X= x2; Y=y2 } } =
        (Math.Abs(x1 - x2), Math.Abs(y1 - y2))
    let inBounds { TopLeft = { X = minX; Y = minY }; BottomRight = { X= maxX; Y= maxY} } { X=x; Y=y } =
        (x >= minX) && (x <= maxX) && (y >= minY) && ( y <= maxY )

type Vector = { DX: int; DY: int }
type Point = Coord * Vector

let step ({ X= x; Y=y}, {DX=dx; DY=dy}) = ({ X = x + dx; Y = y + dy }, {DX=dx; DY=dy} )

let parse input =
    match input with 
    | Regex "position=\<\s*(-?\d+),\s*(-?\d+)\> velocity=\<\s*(-?\d+),\s*(-?\d+)\>" [ x; y; dx; dy; ] ->
        ({ X = Int32.Parse x; Y = Int32.Parse(y)}, { DX = Int32.Parse(dx); DY = Int32.Parse(dy)})
    | _ -> failwith <| sprintf "Unable to parse %s " input 

let render (writer: TextWriter) bounds (points: Coord list) : unit =
    let (width, height) = Bounds.size bounds
    let offSetX = -1 * bounds.TopLeft.X
    let offSetY = -1 * bounds.TopLeft.Y
    let array = Array2D.create width height "."
    let offSetPoints = Seq.map (fun { X = x; Y= y } -> { X = x + offSetX; Y= y + offSetY }) points
    let maxY = height - 1
    let maxX = width - 1

    for { X= x ; Y= y} in offSetPoints do
        if x >= 0 && x <= maxX && y >= 0 && y <= maxY then
            Array2D.set array x y "#"
    for y in 0..maxY do
        for x in 0..maxX do
            writer.Write(Array2D.get array x y)
        writer.WriteLine()

let findBounds (input: Coord list) : Bounds =
    let padding = 5
    let expand { TopLeft = { X = minX; Y = minY }; BottomRight = { X= maxX; Y= maxY} } { X=x; Y=y } =
        {
            TopLeft = {
                X = Math.Min(minX, x);
                Y = Math.Min(minY, y);
            };
            BottomRight = {
                X = Math.Max(maxX, x);
                Y = Math.Max(maxY, y);
            }
        }    
    let result = 
        match input with 
        | { X=x; Y=y }::t -> input |> Seq.fold expand { TopLeft = { X=x; Y=y }; BottomRight = {X=x; Y=y} }   
        | [] -> { TopLeft = { X=0; Y=0 }; BottomRight = {X=0; Y=0} }   
    
    {
        TopLeft = {
            X = result.TopLeft.X - padding;
            Y = result.TopLeft.Y - padding;
        };
        BottomRight = {
            X = result.BottomRight.X + padding;
            Y = result.BottomRight.Y + padding;
        }
    }


let rec animate writer (state: Point list) next i =
    let bounds = state |> List.map fst |> findBounds
    let (width, height) = Bounds.size bounds

    if Math.Abs(width * height) < 1500 then 
        render writer bounds (state |> Seq.map fst |> List.ofSeq)    
        next()
    let nextState = state |> List.map step
    animate writer nextState next (i+1)

let day10A (writer: TextWriter) inputs next =   
    let setup = inputs |> List.ofSeq |> List.map parse    
    animate writer setup next 0
