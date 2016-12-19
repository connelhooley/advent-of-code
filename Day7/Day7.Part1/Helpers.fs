module Helpers

open System
open System.Text.RegularExpressions

let toString x = 
    x.ToString()

let split (separator:string) (x:string) = 
    x.Split([|separator|], StringSplitOptions.RemoveEmptyEntries)

let private matchesToList (matchCollection:MatchCollection) =
    [for regMatch in matchCollection -> regMatch]

let private groupsToList (groupCollection:GroupCollection) =
    [for regGroup in groupCollection -> regGroup]

let regex regExp input =
    let regex = Regex regExp
    regex.Matches input
    |> matchesToList
    |> List.map (fun m -> m.Groups)
    |> List.collect groupsToList
    |> List.map toString