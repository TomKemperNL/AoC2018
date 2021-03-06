﻿module Day10

open System
open System.IO
open TwoD

type Light = Light
type Vector = { DX: int; DY: int }
type Point = Coord * Vector

let step (Coord(x,y), {DX=dx; DY=dy}) = (Coord(x + dx, y + dy), {DX=dx; DY=dy} )

let visualization = Map.ofArray [| (None, "."); (Some Light, "#")|]

let parse input =
    match input with 
    | Regex "position=\<\s*(-?\d+),\s*(-?\d+)\> velocity=\<\s*(-?\d+),\s*(-?\d+)\>" [ x; y; dx; dy; ] ->
        (Coord(Int32.Parse x, Int32.Parse(y)), { DX = Int32.Parse(dx); DY = Int32.Parse(dy)})
    | _ -> failwith <| sprintf "Unable to parse %s " input 

let rec animate writer vis (state: Point list) next i =
    let bounds = state |> List.map fst |> Bounds.find
    let (width, height) = Bounds.size bounds

    if Math.Abs(width * height) < 1500 then 
        render writer vis bounds (state |> Seq.map (fun (c, v) -> (c, Light)) |> Map.ofSeq)    
        next()
    let nextState = state |> List.map step
    animate writer vis nextState next (i+1)

let day10A (writer: TextWriter) vis inputs next =   
    let setup = inputs |> List.ofSeq |> List.map parse    
    animate writer vis setup next 0
