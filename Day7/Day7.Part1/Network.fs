module Network
    
open System
open Helpers

let private ipSupportsTls input =
    input
    |> regexMatch @"[a-z]*(([a-z])(?!\2)([a-z])\3\2)[a-z]*"

let filterHypernets input =
    input
    |> regexMatch @"[a-z]*\[[a-z]*(([a-z])(?!\2)([a-z])\3\2)[a-z]*\][a-z]*"
    |> not

let countValid input =
    input
    |> Seq.filter filterHypernets
    |> Seq.filter ipSupportsTls
    |> Seq.length