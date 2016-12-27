module Helpers

open System

let split (seperator:string) (x:string) =
    x.Split([|seperator|], StringSplitOptions.RemoveEmptyEntries)
    |> List.ofArray

let startsWith (value:string) (x:string) =
    x.StartsWith(value)
    
let toLower (x:string) =
    x.ToLower()