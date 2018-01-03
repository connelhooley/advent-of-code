module HexGrid

type Direction =
| North
| South
| NorthEast
| NorthWest
| SouthEast
| SouthWest

let parseInput (fileContents: string): Direction list =
    let mapDirection = function
        | "n" -> North
        | "s" -> South
        | "ne" -> NorthEast
        | "nw" -> NorthWest
        | "se" -> SouthEast
        | "sw" -> SouthWest
        | s -> failwith (sprintf "Invalid input: %s" s)
    
    fileContents
    |> String.split ','
    |> Seq.map (String.trim >> mapDirection)
    |> List.ofSeq

let getStepsCount (directions: Direction list) =
    let move (x,y) direction =
        match direction with
        | North -> (x, y+1.0)
        | South -> (x, y-1.0)
        | NorthEast -> (x-0.5, y+0.5)
        | NorthWest -> (x+0.5, y+0.5)
        | SouthEast -> (x-0.5, y-0.5)
        | SouthWest -> (x+0.5, y-0.5)
    let x,y = Seq.fold move (0.0, 0.0) directions
    (abs x) + (abs y)