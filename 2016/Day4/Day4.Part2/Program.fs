open System
open System.IO

[<EntryPoint>]
let main argv = 
    File.ReadAllText("input.txt")
    |> Input.parse
    |> Validtor.filterValid
    |> Decrypter.decryptRooms
    |> Output.getPoleSectorIds
    |> Seq.iter (printfn "%i")
    ignore(Console.ReadLine())
    0 // return an integer exit code