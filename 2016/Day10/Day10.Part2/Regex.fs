namespace Regex

module Regex = 

    open System
    open System.Text.RegularExpressions
    open Helpers

    let private regexGroups regExp input =
        let regex = Regex regExp
        let regMatch = regex.Match input
        if regMatch.Success then
            let groups =
                regMatch.Groups
                |> Seq.cast<Group>
                |> Seq.skip 1
                |> Seq.map toString
                |> List.ofSeq
            Some(groups)
        else
            None

    let (|IsInitialize|_|) input =
        let mapGroups groups = 
            (
                groups |> List.item 0 |> Int32.Parse, 
                groups |> List.item 1 |> Int32.Parse
            )
        input 
        |> regexGroups "value (\d+) goes to bot (\d+)"
        |> Option.map mapGroups
            
    let (|IsTransport|_|) input =
        let mapGroups groups = 
            (
                groups |> List.item 0 |> Int32.Parse,
                groups |> List.item 1,
                groups |> List.item 2 |> Int32.Parse,
                groups |> List.item 3,
                groups |> List.item 4 |> Int32.Parse
            )

        input 
        |> regexGroups "bot (\d+) gives low to (bot|output) (\d+) and high to (bot|output) (\d+)"
        |> Option.map mapGroups