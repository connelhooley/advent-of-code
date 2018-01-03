// Learn more about F# at http://fsharp.org

open System
open System.IO

[<EntryPoint>]
let main _ =
    "input.txt"
    |> File.ReadAllText
    |> HexGrid.parseInput
    |> HexGrid.getMaxStepsCount
    |> printfn "%A" 
    ignore(Console.ReadLine())
    0 // return an integer exit code
        