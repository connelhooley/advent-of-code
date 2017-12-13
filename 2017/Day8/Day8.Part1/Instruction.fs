﻿module Instruction

open System.IO
open System.Text.RegularExpressions

type Operation = 
| Increment
| Decrement

type Comparison = 
| GreaterThan
| GreaterThanOrEqualTo
| LessThan
| LessThanOrEqualTo
| Equals
| NotEqualTo

type Instruction = {
    Operation: (string * Operation * int)
    Comparison: (string * Comparison * int)
}

let read fileName =
    let matchLine line =
        let _match = Regex.Match(line, "([a-z]+) (inc|dec) (\-*\d+) if ([a-z]+) (\>|\>\=|\<|\<\=|\=\=|\!\=+) (\-*\d+)")
        {
            Operation = 
                (
                    _match.Groups.[1].Value,
                    (match _match.Groups.[2].Value with
                    | "inc" -> Increment
                    | "dec" -> Decrement
                    | _ -> failwith "Failed to parse operation"),
                    _match.Groups.[3].Value |> int
                )
            Comparison =
                (
                    _match.Groups.[4].Value,
                    (match _match.Groups.[5].Value with
                    | ">" -> GreaterThan
                    | ">=" -> GreaterThanOrEqualTo
                    | "<" -> LessThan
                    | "<=" -> LessThanOrEqualTo
                    | "==" -> Equals
                    | "!=" -> NotEqualTo
                    | _ -> failwith "Failed to parse comparison"),
                    _match.Groups.[6].Value |> int
                )
        }

    fileName
    |> File.ReadAllLines
    |> Array.map matchLine
    |> List.ofArray

let getHighestValueRegister instructions =
    let performInstruction state instruction = 
        let findRegisterValue name =
            match state |> Map.tryFind name with
            | Some value -> value
            | None -> 0
            
        let isConditionMet =
            let (register, comparison, value) = instruction.Comparison
            let registerValue = findRegisterValue register
            match comparison with
            | GreaterThan -> registerValue > value
            | GreaterThanOrEqualTo -> registerValue >= value
            | LessThan -> registerValue < value
            | LessThanOrEqualTo -> registerValue <= value
            | Equals -> registerValue = value
            | NotEqualTo -> registerValue <> value
        
        if isConditionMet then
            let (register, operation, value) = instruction.Operation
            let registerValue = findRegisterValue register
            match operation with
            | Increment ->
                state
                |> Map.add register (registerValue + value)
            | Decrement ->  
                state
                |> Map.add register (registerValue - value)
        else
            state
            
    instructions
    |> List.fold performInstruction Map.empty
    |> Map.toList
    |> List.maxBy snd
    |> snd