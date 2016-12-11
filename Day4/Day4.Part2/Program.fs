open System
open System.IO

[<EntryPoint>]
let main argv = 
    File.ReadAllText("input.txt")
//    @"aaaaa-bbb-z-y-x-123[abxyz]
//    a-b-c-d-e-f-g-h-987[abcde]
//    not-a-real-room-404[oarel]
//    totally-real-room-200[decoy]"
    |> Input.parse
    |> Validtor.filterValid
    |> Output.decryptRooms
    |> printfn "%A"
    ignore(Console.ReadLine())
    0 // return an integer exit code