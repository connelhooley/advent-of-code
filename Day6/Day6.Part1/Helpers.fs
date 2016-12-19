module Helpers

open System

let toCharArray (x:string) = 
    x.ToCharArray()

let toString x = 
    x.ToString()

let notNullOrWhiteSpace (x:string) = 
    x
    |> String.IsNullOrWhiteSpace
    |> not

let split (separator:string) (x:string) = 
    x.Split([|separator|], StringSplitOptions.RemoveEmptyEntries)

let mergeStrings (x:seq<string>) =
    x |> Seq.fold (+) ""