﻿open System
open System.IO

[<EntryPoint>]
let main argv =
    File.ReadAllText("input.txt")
    |> Input.parse
    |> Output.countValid
    |> printfn "%i"
    ignore(Console.ReadLine())
    0 // return an integer exit code