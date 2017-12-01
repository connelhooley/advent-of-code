module Input

open System
open System.Text.RegularExpressions
open Helpers
open Types

let private mapMatchGroups (collection:MatchCollection) =
    [for regMatch in collection -> regMatch.Groups]

let private regex = Regex("([a-z-]+[a-z])\-(\d+)\[([a-z]+)\]")

let private parseRoom (groups: GroupCollection) = 
    {
        encryptedName = groups.[1] 
        |> toString
        |> splitInToChars
        sectorId = groups.[2] 
        |> toString 
        |> Int32.Parse
        checksum = groups.[3] 
        |> toString 
        |> splitInToChars
    }
    
let parse input =
    input
    |> regex.Matches
    |> mapMatchGroups
    |> Seq.map parseRoom