module Code

open Types
open Helpers

let dialPad =  [| [|7;8;9|]; [|4;5;6|]; [|1;2;3|] |]

let dialPadRowContainsNumber (buttonNumber:int) (row:int[]) =
    row
    |> Array.contains buttonNumber

let getDialPadRow (buttonNumber:int) =
    dialPad
    |> Array.findIndex (dialPadRowContainsNumber buttonNumber)

let getDialPadColumn (buttonNumber:int) (row:int) =
    dialPad.[row]
    |> Array.findIndex ((=) buttonNumber)

let getButton (buttonNumber:int) =
    let row = getDialPadRow buttonNumber
    let column = getDialPadColumn buttonNumber row
    (row, column)

let getCode (state:int) (direction:Direction) =
    state

let getCodes (state:string) (row:seq<Direction>) =  
    row 
    |> Seq.fold getCode 5
    |> toString
    |> prepend state

let calculate (input: seq<seq<Direction>>) =
    input
    |> Seq.fold getCodes ""