// Learn more about F# at http://fsharp.org

open System

[<EntryPoint>]
let main _ =
    Stream.read "input.txt"
    |> Stream.countGroupScores
    |> printfn "%i"
    ignore(Console.ReadLine())
    0 // return an integer exit code
