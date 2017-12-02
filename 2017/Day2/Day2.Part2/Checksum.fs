module Checksum

let parse (input: int [] []) =
    let cartesianRow row = 
        row
        |> Array.collect (fun x -> 
            row
            |> Array.map (fun y -> x, y)
            |> Array.filter (fun (x, y) -> x <> y))

    let filterRow row = 
        row
        |> Array.map (fun (x, y) -> (x |> double) / (y |> double))
        |> Array.find (fun x -> x % 1.0 = 0.0)
 
    input
    |> Array.map (cartesianRow >> filterRow)
    |> Array.sum
    |> int