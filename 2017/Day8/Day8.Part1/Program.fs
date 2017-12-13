// Learn more about F# at http://fsharp.org

open System

[<EntryPoint>]
let main _ =
    Instruction.read "input.txt"
    |> Instruction.getHighestValueRegister
    |> printf "%A"
    ignore(Console.ReadLine())
    0 // return an integer exit code
