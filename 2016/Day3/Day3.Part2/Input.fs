module Input

open System
open System.IO
open Helpers

let private mapTriangle points =
    points
    |> List.ofSeq
    |> (fun point -> (point.[0], point.[1], point.[2]))

let private orderByColumn columnCount points =
    points
    |> Seq.mapi (fun i p -> (i, p))
    |> Seq.groupBy (fun (i,p) -> i % columnCount)
    |> Seq.collect (fun (i,p) -> p)
    |> Seq.map (fun (i, p) -> p)

let parse (input:string) =
    split " " input
    |> Seq.map Int32.Parse
    |> orderByColumn 3
    |> Seq.chunkBySize 3
    |> Seq.map mapTriangle