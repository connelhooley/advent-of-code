module Network
    
open System
open Helpers

let private getInsideBrackets input =
    input
    |> regexGroups @"[^\[\]]+(?=])"

let private getOutsideBrackets input =
    input
    |> regexGroups @"\w+(?=\[|\s|$)"

let private invertAba aba =
    let chars = toCharArray aba
    sprintf "%c%c%c" chars.[1] chars.[0] chars.[1]

let private getAba (chars:char[]) i =
    let first = chars.[i];
    let second = chars.[i+1];
    let third = chars.[i+2];
    if(first = third && first <> second) then
        Some(sprintf "%c%c%c" first second third)
    else
        None

let private getAbas input =
    let chars = toCharArray input
    [0..chars.Length-3]
    |> Seq.map (getAba chars)
    |> Seq.choose id
    
let private containsAnyOf required input =
    required
    |> Seq.exists (contains input)
 
let private ipSupportsSsl ipAddress =
    let insideBrackets = getInsideBrackets ipAddress
    let outsideBrackets = getOutsideBrackets ipAddress
    let abas = insideBrackets |> Seq.collect getAbas
    let possibleBabs = abas |> Seq.map invertAba
    outsideBrackets
    |> Seq.exists (containsAnyOf possibleBabs)
    
let countValid (input:seq<string>) =
    input
    |> Seq.filter ipSupportsSsl
    |> Seq.length