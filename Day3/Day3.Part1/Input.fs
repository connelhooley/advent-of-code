module Input

open System
open System.IO
open Helpers
let private parseTriangle (sides:list<int>) =
    (sides.[0], sides.[1], sides.[2])

let private mapRow (row:string) =
    split " " row
    |> Seq.map Int32.Parse
    |> List.ofSeq
    |> parseTriangle

let private parseRows (input:string) = 
    let rec recurser (reader:StringReader) = seq {
        let line = reader.ReadLine()
        if not(String.IsNullOrWhiteSpace(line)) then 
            yield line
            yield! recurser reader
        else
            reader.Dispose()
    }
    recurser (new StringReader(input))

let parse (input:string) =
    parseRows input
    |> Seq.map mapRow