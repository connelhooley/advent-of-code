open System
open System.IO
open Instructions
open Results

[<EntryPoint>]
let main argv =
    File.ReadAllText "input.txt"
    |> Instructions.parse
    |> Instructions.perform
    |> Results.findComparison (61, 17)
    |> printfn "Robot number %i compared 61 and 17"
    ignore(Console.ReadLine())
    0 // return an integer exit code