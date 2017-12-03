open System

[<EntryPoint>]
let main argv = 
    Spiral.getCoordinates 347991
    |> Steps.calculate
    |> printfn "%A"
    ignore(Console.ReadLine())
    0 // return an integer exit code
