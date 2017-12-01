module Input

open System
open Types
open Helpers

let private mapTurn (s:string) =
    match s.Substring(0,1) with
    | "R" -> R
    | "L" -> L
    | _ -> failwith "Invalid input"

let private mapDistance s =
    s
    |> substring 1 (s.Length-1)
    |> Int32.Parse

let private mapInstruction s =
    (mapTurn s, mapDistance s)

let private mapStep turn i =
    match i with
    | 1 -> 
        match turn with
        | L -> Left
        | R -> Right
    | _ -> Straight

let private mapSteps (turn, distance) =
    [1..distance]
    |> Seq.map (mapStep turn)

let parse input =
    input
    |> split [|','; ' '|]
    |> Seq.ofArray
    |> Seq.filter isNotNullOrWhiteSpace
    |> Seq.map mapInstruction
    |> Seq.map mapSteps
    |> Seq.collect id