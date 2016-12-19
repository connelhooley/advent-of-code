module Password

open System
open Helpers

let private printResult result = 
   result
   |> mergeStrings
   |> (printfn "%s")

let private getValue (hash:string) =
    hash.Substring(6, 1)

let private getPosition (hash:string) =
    hash.Substring(5, 1)
    |> parseInt

let private isPositionValid (result:string[]) position =
    position >= 0 && position <= 7 && result.[position] = "_"

let private populate (result:string[]) (hash:string) = 
    if (hash.StartsWith "00000") then
        match hash |> getPosition with
        | Some(position) when isPositionValid result position -> 
            let value = hash |> getValue
            result.[position] <- value
            printResult result
        | _ -> ()

let private getHash doorId number = 
    sprintf "%s%i" doorId number
    |> md5Hash
    |> toLower

let private isFinished result = 
   result
   |> Seq.forall ((<>) "_")

let calculate doorId =
    let result = Array.create 8 "_"
    let populateResult = populate result
    let getDoorIdHash = getHash doorId
    let rec loop number =
        populateResult (getDoorIdHash number)
        if (isFinished result) then
            result |> mergeStrings
        else
            loop (number + 1)
    loop 0