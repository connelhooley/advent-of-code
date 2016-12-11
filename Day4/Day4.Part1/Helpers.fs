module Helpers

open System

let split (separator:string) (x:string) = 
    x.Split([|separator|], StringSplitOptions.RemoveEmptyEntries)
    
let splitInToChars (x:string) = 
    x.ToCharArray()

let toString x =
    x.ToString()
    
let isInt (x:char) =
    match Int32.TryParse(x.ToString()) with
    | (isParsed, value) -> isParsed
