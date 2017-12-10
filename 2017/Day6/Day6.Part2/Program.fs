// Learn more about F# at http://fsharp.org

open System

[<EntryPoint>]
let main _ =
    //Reallocation.read "input.txt"
    [0;2;7;0]
    |> Reallocation.countReallocateLoopSize
    |> Console.WriteLine
    ignore(Console.ReadLine())
    0 // return an integer exit code