module Helpers

open System

let split (separator:string) (x:string) = 
    x.Split([|separator|], StringSplitOptions.RemoveEmptyEntries)
    
let splitInToChars (x:string) = 
    x.ToCharArray()

let toString x =
    x.ToString()

let toLower (x:string) =
    x.ToLower()

let contains value (x:string) =
    x.Contains(value)
