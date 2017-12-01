open System
open System.IO

[<EntryPoint>]
let main argv = 
    File.ReadAllLines "input.txt"
    |> Network.countValid
    |> printfn "%A"
    ignore(Console.ReadLine())
    0 // return an integer exit code