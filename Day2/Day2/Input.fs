module Input

open System

open Types
open Helpers

let mapDirection char = 
    match char with
    | 'U' -> Up
    | 'R' -> Right
    | 'D' -> Down
    | 'L' -> Left
    | _ -> failwith "Unexpected char in input"

let mapDirections line = 
    line
    |> splitInToChars
    |> Seq.ofArray
    |> Seq.map mapDirection

let parse input =
    input
    |> split Environment.NewLine
    |> Seq.ofArray
    |> Seq.map mapDirections