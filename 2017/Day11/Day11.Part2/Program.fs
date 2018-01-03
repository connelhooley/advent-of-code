// Learn more about F# at http://fsharp.org

open System
open System.IO

[<EntryPoint>]
let main _ =
    //"ne,ne,ne" //3
    //"ne,ne,sw,sw" //0
    //"ne,ne,s,s" //2
    //"se,sw,se,sw,sw" //3
    "input.txt"
    |> File.ReadAllText
    |> HexGrid.parseInput
    |> HexGrid.getStepsCount
    |> printfn "%A" 
    ignore(Console.ReadLine())
    0 // return an integer exit code
        