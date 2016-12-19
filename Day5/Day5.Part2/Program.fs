open System

[<EntryPoint>]
let main argv = 
    printfn "Calculating password"
    "cxdnnyjw"
    |> Password.calculate
    |> (printfn "Password is: %s")
    ignore(Console.ReadLine())
    0 // return an integer exit code