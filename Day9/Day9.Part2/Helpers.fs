module Helpers

open System
open System.Text.RegularExpressions

let trim (x:string) =
    x.Trim ()

let toCharArray (x:string) =
    x.ToCharArray ()
    
let toString x =
    x.ToString ()
    
let toLower (x:string) =
    x.ToLower ()

let split (seperator:string) (x:string) =
    x.Split ([|seperator|], StringSplitOptions.RemoveEmptyEntries)

let regexGroups regExp input =
    let groupList (regMatch:Match) =
        regMatch.Groups
        |> Seq.cast<Group>
        |> List.ofSeq
        |> List.map toString
    let regex = Regex regExp
    regex.Matches input
    |> Seq.cast<Match>
    |> List.ofSeq
    |> List.map (groupList)