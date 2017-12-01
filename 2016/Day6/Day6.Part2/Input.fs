module Input

open System
open Helpers

let private columns columnCount items =
    items
    |> Seq.mapi (fun i p -> (i % columnCount, p))
    |> Seq.groupBy fst
    |> Seq.map snd
    |> Seq.map (Seq.map snd)

let parse input =
    input
    |> toCharArray
    |> Seq.map toString
    |> Seq.filter notNullOrWhiteSpace
    |> columns 8