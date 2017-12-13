module Stream

open System.IO

type private State = {
    Depth: int
    TotalScore: int
    IgnoreCurrent: bool
    IsInsideGarbage: bool
}

let read fileName = (File.ReadAllText fileName).ToCharArray()

let countGroupScores stream =
    let processItem state item =
        match state with
        | state when state.IgnoreCurrent -> 
            { state with IgnoreCurrent = false }
        | state when state.IsInsideGarbage -> 
            match item with
            | '!' -> { state with IgnoreCurrent = true }
            | '>' -> { state with IsInsideGarbage = false }
            | _ -> state
        | _ ->
            match item with
            | '!' -> { state with IgnoreCurrent = true }
            | '<' -> { state with IsInsideGarbage = true }
            | '{' -> 
                { state with 
                    Depth = state.Depth + 1
                    TotalScore = state.TotalScore +  state.Depth + 1
                }
            | '}' -> 
                { state with 
                    Depth = state.Depth - 1
                }
            | _ -> state
        
    let initalState = {
        Depth = 0;
        TotalScore = 0;
        IgnoreCurrent = false
        IsInsideGarbage = false
    }

    let result = 
        stream
        |> Array.fold processItem initalState
    
    result.TotalScore