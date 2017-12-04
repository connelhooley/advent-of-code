open System

[<EntryPoint>]
let main _ = 
    Spiral.getFirstValueGreaterThan 347991
    |> printfn "%A"
    ignore(Console.ReadLine())
    0 // return an integer exit code
