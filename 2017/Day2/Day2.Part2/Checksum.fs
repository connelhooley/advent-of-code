module Checksum

let parse input =
    let cartesian items = 
        items |> Array.collect (fun x -> items |> Array.map (fun y -> x, y))
    
    input
    |> Array.map cartesian
    |> Array.map (fun row -> 
        row 
        |> Array.map (fun (a,b) -> a / b)
        |> Array.filter (fun i -> i <> 1)
        |> Array.filter (fun i -> i % 2  = 0))
    
    //|> Array.sum