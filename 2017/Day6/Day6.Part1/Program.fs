// Learn more about F# at http://fsharp.org

open System

[<EntryPoint>]
let main _ =
    Reallocation.read "input.txt"
    |> Reallocation.countReallocates
    |> Console.WriteLine
    ignore(Console.ReadLine())
    0 // return an integer exit code