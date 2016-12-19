module Output

open System
open Helpers

let mostCommon (items:seq<string>) =
    items
    |> Seq.countBy id
    |> Seq.sortBy fst
    |> Seq.minBy snd
    |> fst

let decode input =
    input
    |> Seq.map mostCommon
    |> mergeStrings