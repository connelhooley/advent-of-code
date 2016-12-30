namespace Output

module Output =
    
    open Destination
    open OutputBin

    let print (robots, outputBin) =
        outputBin
        |> Destination.items
        |> List.take 3
        |> List.map OutputBin.getMicrochip
        |> List.fold (*) 1
        |> printfn "Microchips inside output bins, multiplied together is %i"