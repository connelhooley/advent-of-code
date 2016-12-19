open System
open System.IO

[<EntryPoint>]
let main argv =
    File.ReadAllText "input.txt"
    |> Input.parse
    |> Output.decode
    |> printfn "%s"
    ignore(Console.ReadLine())
    0 // return an integer exit code