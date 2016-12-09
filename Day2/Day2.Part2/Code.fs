module Code

open Types
open Helpers

let private buttons =
    [
        { coordinate = { y = 5; x = 3; }; value= "1" };
        { coordinate = { y = 4; x = 2; }; value= "2" };
        { coordinate = { y = 4; x = 3; }; value= "3" };
        { coordinate = { y = 4; x = 4; }; value= "4" };
        { coordinate = { y = 3; x = 1; }; value= "5" };
        { coordinate = { y = 3; x = 2; }; value= "6" };
        { coordinate = { y = 3; x = 3; }; value= "7" };
        { coordinate = { y = 3; x = 4; }; value= "8" };
        { coordinate = { y = 3; x = 5; }; value= "9" };
        { coordinate = { y = 2; x = 2; }; value= "A" };
        { coordinate = { y = 2; x = 3; }; value= "B" };
        { coordinate = { y = 2; x = 4; }; value= "C" };
        { coordinate = { y = 1; x = 3; }; value= "D" };
    ]

let private isValidCoordinate coordinate button =
    coordinate = button.coordinate

let private moveToNextButton currentButton nextCoordinate  =
    let found = Seq.tryFind (isValidCoordinate nextCoordinate) buttons
    match found with
    | Some(nextButton) -> nextButton
    | None -> currentButton

let private getNextButton current direction =
    match direction with 
    | Up -> { current.coordinate with y = current.coordinate.y+1 }
    | Right -> { current.coordinate with x = current.coordinate.x+1 }
    | Down -> { current.coordinate with y = current.coordinate.y-1 }
    | Left -> { current.coordinate with x = current.coordinate.x-1 }
    |> moveToNextButton current

let private isButtonWithValue value button =
    button.value = value

let private getButton value =
    buttons
    |> Seq.find (isButtonWithValue value)

let private getCode state instruction =
    match instruction with
    | Read -> 
        { state with result = state.current.value :: state.result }
    | Direction(direction) ->
        { state with current = getNextButton state.current direction }

let private getResult finalState =
    finalState.result
    |> List.rev

let calculate input =
    let startingState = {
        current = getButton "5"
        result = []
    }
    input
    |> Seq.fold getCode startingState
    |> getResult