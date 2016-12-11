open System
open System.IO

//361724

[<EntryPoint>]
let main argv = 
    File.ReadAllText("input.txt")
    |> Input.parse
    |> Validtor.filterValid
    |> Output.sumSectorIds
    |> printfn "%i"
    ignore(Console.ReadLine())
    0 // return an integer exit code