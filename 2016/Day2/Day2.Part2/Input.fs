module Input

open System

open Types
open Helpers

let private mapDirection char = 
    match char with
    | 'U' -> Instruction.Direction(Up)
    | 'R' -> Instruction.Direction(Right)
    | 'D' -> Instruction.Direction(Down)
    | 'L' -> Instruction.Direction(Left)
    | _ -> failwith "Unexpected char in input"

let private appendRead instructions = 
    instructions @ [Read]

let private mapDirections line = 
    line
    |> splitInToChars
    |> List.ofArray
    |> List.map mapDirection

let parse input =
    input
    |> split Environment.NewLine
    |> Seq.ofArray
    |> Seq.map mapDirections
    |> Seq.collect appendRead