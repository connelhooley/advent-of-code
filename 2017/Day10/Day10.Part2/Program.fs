// Learn more about F# at http://fsharp.org

open System

[<EntryPoint>]
let main _ =
    "input.txt"
    //"input2.txt"
    |> KnotHash.parse
    //|> KnotHash.perform [0 .. 4]
    |> KnotHash.perform [0 .. 255]
    |> KnotHash.multiplyFirstTwoNumbers
    |> printf "%i"
    ignore(Console.ReadLine())
    0 // return an integer exit code
