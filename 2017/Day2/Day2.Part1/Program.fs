open System

[<EntryPoint>]
let main argv = 
    "input.txt"
    |> Grid.read 
    |> Checksum.parse
    |> printfn "%A"
    ignore(Console.ReadLine())
    0 // return an integer exit code
