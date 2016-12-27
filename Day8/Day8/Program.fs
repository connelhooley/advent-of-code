open System
open System.IO
open Instruction
open Screen

[<EntryPoint>]
let main argv = 
    File.ReadAllLines "input.txt"
    |> Seq.map InstructionModule.parse
    |> Seq.fold ScreenModule.perform (ScreenModule.create 50 6)
    |> ScreenModule.print
    ignore(Console.ReadLine())
    0 // return an integer exit code