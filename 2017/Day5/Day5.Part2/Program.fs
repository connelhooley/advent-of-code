// Learn more about F# at http://fsharp.org

open System

[<EntryPoint>]
let main _ =
    Instructions.readAll "input.txt"
    |> Instructions.perform
    |> Console.WriteLine
    ignore(Console.ReadLine())
    0 // return an integer exit code
