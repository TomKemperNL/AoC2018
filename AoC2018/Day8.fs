module Day8

open System

[<Struct>]
type Header =
    | Header of int*int

[<Struct>]
type Metadata = 
    | Metadata of int

[<Struct>]
type Node = {
    Header: Header;
    Metadata: Metadata list;
    Children: Node list
}

let parse (input: string) : Node =    
    let rec parseMetadata input nr acc = 
        match nr with 
        | 0 -> (List.rev acc, input)
        | n -> 
            match input with
            | h :: t -> parseMetadata t (n-1) ((Metadata h) :: acc)
            | _ -> failwith "cant make metadate out of this"
    
    let parseHeader input = 
        match input with 
        | (a:: b:: t) -> (Header(a,b), t)
        | _ -> failwith "cant make a header out of this"
    
    let rec parseChildren input nr acc : (Node list*int list)=
        match nr with 
        | 0 -> (List.rev acc, input)
        | _ ->
            let (newNode,rest) = parseNode input

            parseChildren rest (nr - 1) (newNode::acc)
    and parseNode input =
        let (header, childRest) = parseHeader input
        match header with 
        | Header (nrChildren, metaNr) ->
            let (children, metaRest) = parseChildren childRest nrChildren []
            let (meta, leftAfterMeta) = parseMetadata metaRest metaNr []
            let newNode = {
                Header = header;
                Metadata = meta;
                Children = children
            }        
            (newNode, leftAfterMeta)
    let tokens = input.Split(' ') |> Array.map Int32.Parse |> List.ofArray
    parseNode tokens |> fst


let day8A input = 
    let rec sumNode total (node: Node) = 
        let mine = List.map (fun (Metadata m) -> m) node.Metadata |> List.sum
        let theirs = List.fold sumNode 0 node.Children
        total + mine + theirs

    let tree = parse input
    sumNode 0 tree

let day8B input = 
    let rec sumNode (total:int) (node: Node) = 
        if node.Children.Length = 0 then
            total + (List.map (fun (Metadata m) -> m) node.Metadata |> List.sum)
        else
            let getNode (i: int) :Node= List.item (i-1) node.Children 
                
            let childTotal: int = node.Metadata |>
                                    List.map (fun (Metadata m) -> m) |> 
                                    List.filter (fun i -> i > 0 && i <= node.Children.Length) |> 
                                    List.map (fun i -> sumNode 0 (getNode i)) |> 
                                    List.sum    
            childTotal + total
    let tree = parse input
    sumNode 0 tree
