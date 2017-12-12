// Learn more about F# at http://fsharp.org

open System

[<EntryPoint>]
let main _ =
    Tower.read "input.txt"
    |> Tower.findDesiredChildWeight
    |> printf "%A"
    ignore(Console.ReadLine())
    0 // return an integer exit code