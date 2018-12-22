module Day10

open System
open System.IO

type Coord = { X: int; Y: int;}
type Bounds = { TopLeft: Coord; BottomRight: Coord}
module Bounds =
    let size { TopLeft= { X =x1; Y=y1 }; BottomRight= { X= x2; Y=y2 } } =
        (Math.Abs(x1 - x2), Math.Abs(y1 - y2))

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
    let offSetX = Math.Abs(bounds.TopLeft.X)
    let offSetY = Math.Abs(bounds.TopLeft.Y)
    let array = Array2D.create width height "."
    for { X= x ; Y= y} in points do
        Array2D.set array (x + offSetX) (y+ offSetY) "#"
    for y in 0..(height - 1) do
        for x in 0..(width - 1) do
            writer.Write(Array2D.get array x y)
        writer.WriteLine()

let day10A (writer: TextWriter) inputs =
    let bounds = { TopLeft = { X = -20; Y= -20}; BottomRight = { X = 20; Y = 20}}
    let setup = Seq.map parse inputs
    render writer bounds (setup |> Seq.map fst |> List.ofSeq)