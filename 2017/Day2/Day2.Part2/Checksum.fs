module Checksum

let parse (input: int [] []) =
    let cartesian items = 
        items 
        |> Array.collect (fun x -> 
            items 
            |> Array.map (fun y -> x, y)
            |> Array.filter (fun (x, y) -> x <> y))
    
    input
    |> Array.map cartesian
    |> Array.map (fun row -> 
        row
        |> Array.map (fun (x, y) -> (x |> double) / (y |> double))
        |> Array.find (fun x -> x % 1.0 = 0.0))
    |> Array.sum
    |> int