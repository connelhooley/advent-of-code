module Helpers

open System

let split separators (x:string) = 
    x.Split(separators)
    
let substring startIndex length (x:string) = 
    x.Substring(startIndex, length)

let isNotNullOrWhiteSpace (x:string) = 
    x
    |> String.IsNullOrWhiteSpace
    |> not
