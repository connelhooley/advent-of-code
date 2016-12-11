module Validtor

open System
open Types

let private count haystack needle =
    haystack
    |> Seq.filter ((=) needle)
    |> Seq.length

let private mapChar encryptedName c =
    (count encryptedName c, c)

let private mapGrouping (key, items) =
    items
    |> Seq.sort

let private mapString chars =
    String(Array.ofSeq chars)

let private compare chars1 chars2 = 
    mapString chars1 = mapString chars2

let private notDash c = 
    c = '-' |> not

let private removeDashes chars = 
    chars
    |> Seq.filter notDash

let private isValid room =
    let nameNoDashes = removeDashes room.encryptedName
    nameNoDashes
    |> Seq.distinct
    |> Seq.map (mapChar nameNoDashes)
    |> Seq.sortByDescending fst
    |> Seq.groupBy fst
    |> Seq.map mapGrouping
    |> Seq.collect id
    |> Seq.map snd
    |> Seq.take 5
    |> (compare room.checksum)

let filterValid rooms =
    rooms
    |> Seq.filter isValid