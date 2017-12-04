open System
open System.IO

[<EntryPoint>]
let main _ = 
    File.ReadAllLines "input.txt"
    |> Array.filter Passphrase.isValid
    |> Array.length
    |> printfn "%A"
    ignore(Console.ReadLine())
    0 // return an integer exit code