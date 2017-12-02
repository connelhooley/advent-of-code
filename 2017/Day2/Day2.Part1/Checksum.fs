module Checksum

let parse input =
    input
    |> Array.map (fun row -> (row |> Array.max) - (row |> Array.min))
    |> Array.sum