module Stream

open System.IO

type private State = {
    TotalGarbageCount: int
    IgnoreCurrent: bool
    IsInsideGarbage: bool
}

let read fileName = (File.ReadAllText fileName).ToCharArray()

let countItemsInGarbage stream =
    let processItem state item =
        match state with
        | state when state.IgnoreCurrent -> 
            { state with IgnoreCurrent = false }
        | state when state.IsInsideGarbage -> 
            match item with
            | '!' -> { state with IgnoreCurrent = true }
            | '>' -> { state with IsInsideGarbage = false }
            | _ -> { state with TotalGarbageCount = state.TotalGarbageCount + 1 }
        | _ ->
            match item with
            | '!' -> { state with IgnoreCurrent = true }
            | '<' -> { state with IsInsideGarbage = true }
            | _ -> state
        
    let initalState = {
        TotalGarbageCount = 0;
        IgnoreCurrent = false
        IsInsideGarbage = false
    }

    let result = 
        stream
        |> Array.fold processItem initalState
    
    result.TotalGarbageCount