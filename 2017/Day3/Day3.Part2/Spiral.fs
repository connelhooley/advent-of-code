module Spiral

type private Direction =
| Right
| Up
| Left
| Down

type private Instruction =
| Turn
| Move

type private Coordinates = {
    x: int
    y: int
}

type private State = {
    Value: int
    Values: Map<Coordinates, int>
    Coordinates: Coordinates
    Direction: Direction
}

let getFirstValueGreaterThan input = 
    let rec instructions = 
        let rec loop size = seq {
            yield! Seq.init size (fun _ -> Move)
            yield Turn
            yield! Seq.init size (fun _ -> Move)
            yield Turn
            yield! loop (size+1)
        }
        loop 1

    let performInstruction state instruction =
        match instruction with
        | Move -> 
            let coordinates = 
                match state.Direction with
                | Right ->
                    { state.Coordinates with x =  state.Coordinates.x+1 }
                | Up ->
                    { state.Coordinates with y =  state.Coordinates.y+1 }
                | Left ->
                    { state.Coordinates with x =  state.Coordinates.x-1 }
                | Down ->
                    { state.Coordinates with y =  state.Coordinates.y-1 }

            let neighbourCoordinates = [|
                { coordinates with x =  coordinates.x+1 }
                { coordinates with y =  coordinates.y+1 }
                { coordinates with x =  coordinates.x-1 }
                { coordinates with y =  coordinates.y-1 }
                { coordinates with x =  coordinates.x+1; y =  coordinates.y+1 }
                { coordinates with x =  coordinates.x+1; y =  coordinates.y-1 }
                { coordinates with x =  coordinates.x-1; y =  coordinates.y+1 }
                { coordinates with x =  coordinates.x-1; y =  coordinates.y-1 }
            |]

            let value = 
                state.Values
                |> Map.filter (fun key _ -> neighbourCoordinates |> Array.contains key)
                |> Map.toSeq
                |> Seq.map snd
                |> Seq.sum

            let values = 
                state.Values 
                |> Map.add coordinates value

            { state with Value =  value; Values = values; Coordinates = coordinates}
        | Turn -> 
            match state.Direction with
            | Down -> 
                { state with Direction = Right }
            | Right -> 
                { state with Direction = Up }
            | Up -> 
                { state with Direction = Left }
            | Left -> 
                { state with Direction = Down }

    let initialValue = 1;
    let initalCoordinate = {x = 0; y = 0 }
    let initalValues = Map.empty |> Map.add initalCoordinate initialValue
    let initalDirection = Right
    let initalState = {
        Value = initialValue
        Values = initalValues
        Coordinates = initalCoordinate
        Direction = initalDirection
    }
    let result = 
        instructions
        |> Seq.scan performInstruction initalState
        |> Seq.find (fun s -> s.Value > input)
    result.Value
    