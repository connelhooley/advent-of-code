open System
open System.IO
open Instructions
open Output

[<EntryPoint>]
let main argv =
    File.ReadAllLines "input.txt"
    |> Instructions.parse
    |> Instructions.perform
    |> Output.print
    ignore(Console.ReadLine())
    0 // return an integer exit code