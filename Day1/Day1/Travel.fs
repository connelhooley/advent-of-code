module Travel

open Types

let private center = {
    x = 0;
    y = 0;
}

let private startState = TravelingState {
    direction = North;
    position = center
    history = [center]
}

let private rotate direction step = 
    match step with
    | Right -> 
        match direction with
            | North -> East
            | East -> South
            | South -> West
            | West -> North
    | Left ->
        match direction with
            | North -> West
            | East -> North
            | South -> East
            | West -> South
    | Straight -> direction

let private move position direction = 
    match direction with
    | North -> 
        { position with y = position.y+1} 
    | East -> 
        { position with x = position.x+1}
    | South -> 
        { position with y = position.y-1}
    | West -> 
        { position with x = position.x-1} 

let updateState position direction history isSecondVisit = 
    if isSecondVisit 
    then 
        ArrivedState {
            position = position;
        }
    else
        TravelingState {
            direction = direction;
            position = position;
            history = position :: history
        }

let private travel state step  = 
    match state with
        | ArrivedState(_) -> state
        | TravelingState(current) ->
            let direction = rotate current.direction step
            let position = move current.position direction
            current.history
            |> Seq.contains position
            |> updateState position direction current.history

let private getPosition state = 
    match state with
    | ArrivedState(current) -> current.position
    | TravelingState(current) -> current.position

let toDestination steps =
    steps
    |> Seq.fold travel startState
    |> getPosition
