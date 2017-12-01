module Helpers

open System

let trim (x:string) =
    x.Trim()

let toCharArray (x:string) =
    x.ToCharArray()
    
let toString x =
    x.ToString()
    
let toLower (x:string) =
    x.ToLower()

let split (seperator:string) (x:string) =
    x.Split([|seperator|], StringSplitOptions.RemoveEmptyEntries)