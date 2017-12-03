module Spiral

type private Direction =
| Right
| Up
| Left
| Down

type private Instruction =
| Turn
| Move

type private State = {
    Value: int
    Coordinates: int * int
    Direction: Direction
}

let getCoordinates input = 
    let rec instructions = 
        let rec loop size = seq {
            yield! Seq.init size (fun i -> Move)
            yield Turn
            yield! Seq.init size (fun i -> Move)
            yield Turn
            yield! loop (size+1)
        }
        loop 1

    let performInstruction state instruction =
        match instruction with
        | Move -> 
            match state.Coordinates with
                | (x,y)  -> 
                    match state.Direction with
                    | Right ->
                        {state with Value = state.Value+1; Coordinates = (x+1,y)}
                    | Up ->
                        {state with Value = state.Value+1; Coordinates = (x,y+1)}
                    | Left ->
                        {state with Value = state.Value+1; Coordinates = (x-1,y)}
                    | Down ->
                        {state with Value = state.Value+1; Coordinates = (x,y-1)}
        | Turn -> 
            match state.Direction with
            | Down -> 
                {state with Direction = Right}
            | Right -> 
                {state with Direction = Up}
            | Up -> 
                {state with Direction = Left}
            | Left -> 
                {state with Direction = Down}
    let state = {
        Value = 1
        Coordinates = (0,0)
        Direction = Right
    }
    (
        instructions
        |> Seq.scan performInstruction state
        |> Seq.find (fun s -> s.Value = input)
    ).Coordinates