module Code

open Types
open Helpers

let createCoordinate y x = {y=y; x=x}

let createCoordinateRow columnCount rowNumber =
    [1..columnCount]
    |> Seq.map (createCoordinate rowNumber)

let createCoordinates rowCount columnCount =
    [1..rowCount]
    |> Seq.rev
    |> Seq.map (createCoordinateRow columnCount)
    |> Seq.collect id

let moveCoordinate coordinates currentCoordinate newCoordinate =
    let found = Seq.tryFind ((=) newCoordinate) coordinates
    match found with
    | Some(item) -> item
    | None -> currentCoordinate

let getNextCoordinate coordinates current direction =
    match direction with 
    | Up -> { current with y = current.y+1 }
    | Right -> { current with x = current.x+1 }
    | Down -> { current with y = current.y-1 }
    | Left -> { current with x = current.x-1 }
    |> moveCoordinate coordinates current

let getNumber coordinates cordinate =
    let found = Seq.tryFindIndex ((=) cordinate) coordinates
    match found with
    | Some(item) -> item+1
    | None -> failwith "Invalid co-ordinate"

let getCoordinate coordinates number =
    let found = Seq.tryItem (number-1) coordinates
    match found with
    | Some(item) -> item
    | None -> failwith "Invalid number"

let getCodes (coordinates:seq<Coordinate>) (state:State) (instruction:Instruction) =
    match instruction with
    | Read -> 
        { state with result = getNumber coordinates state.current :: state.result }
    | Direction(direction) ->
        { state with current = getNextCoordinate coordinates state.current direction }

let getResult finalState =
    finalState.result
    |> List.rev

let calculate (input: seq<Instruction>) =
    let coordinates = createCoordinates 3 3
    let startingState = {
        current = getCoordinate coordinates 5
        result = []
    }
    input
    |> Seq.fold (coordinates |> getCodes) startingState
    |> getResult