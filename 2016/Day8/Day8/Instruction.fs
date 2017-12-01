namespace Instruction

open System
open Helpers

type Instruction =
    | Rectangle of width: int * height: int
    | RotateRow of row: int * amount: int
    | RotateColumn of column: int * amount: int

module InstructionModule =
    let parse line = 
        let parseRect line =
            let xy = 
                line 
                |> toLower
                |> split " "
                |> List.item 1
                |> split "x"
            (
                xy |> List.item 0 |> Int32.Parse,
                xy |> List.item 1 |> Int32.Parse
            )
        
        let parseRotate line =
            let splitLine = 
                line 
                |> toLower 
                |> split " " 
            (
                splitLine.[2] |> split "=" |> List.item 1 |> Int32.Parse, 
                splitLine.[4] |> Int32.Parse
            )

        match line with
        | line when line |> startsWith "rect" -> 
            line 
            |> parseRect 
            |> Rectangle
        | line when line |> startsWith "rotate row" -> 
            line 
            |> parseRotate 
            |> RotateRow
        | line when line |> startsWith "rotate column" -> 
            line 
            |> parseRotate 
            |> RotateColumn
        | _ -> 
            failwith (line |> sprintf "Invalid line in input %s")