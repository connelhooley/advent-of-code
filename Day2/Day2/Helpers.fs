module Helpers

open System

let split (separator:string) (x:string) = 
    x.Split([|separator|], StringSplitOptions.RemoveEmptyEntries)
    
let splitInToChars (x:string) = 
    x.ToCharArray()
    
let prepend (y:string) (x:string) =
    x + y

let toString (x:int) =
    x.ToString()