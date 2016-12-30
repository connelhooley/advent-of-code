module Helpers

open System
open System.Text.RegularExpressions

let toString x =
    x.ToString ()

let isNotEmpty (x:string) =
    (not << String.IsNullOrEmpty) x

let regexGroups regExp input =
    let groupList (regMatch:Match) =
        regMatch.Groups
        |> Seq.cast<Group>
        |> Seq.skip 1
        |> Seq.map toString
        |> List.ofSeq
    let regex = Regex regExp
    regex.Matches input
    |> Seq.cast<Match>
    |> Seq.map groupList
    |> List.ofSeq