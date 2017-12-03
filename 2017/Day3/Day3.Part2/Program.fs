open System

[<EntryPoint>]
let main _ = 
    Spiral.getCoordinates 347991
    |> printfn "%A"
    ignore(Console.ReadLine())
    0 // return an integer exit code
