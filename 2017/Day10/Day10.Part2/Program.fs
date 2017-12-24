// Learn more about F# at http://fsharp.org

open System
open System.IO

[<EntryPoint>]
let main _ =
    "input.txt"
    |> File.ReadAllText
    |> KnotHash.parseRound
    |> KnotHash.sparseHash
    |> KnotHash.denseHash
    |> printf "%s"
    ignore(Console.ReadLine())
    0 // return an integer exit code
