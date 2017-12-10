module Reallocation

open System.IO
open System

let read fileName =
    File.ReadAllText(fileName).Split()
    |> Array.filter (not << String.IsNullOrWhiteSpace)
    |> Array.map int
    |> List.ofArray

let countReallocateLoopSize memoryBanks = 
    let reallocate index value banks =
        let rec indexSeq = seq {
            yield! [index+1 .. (List.length banks)-1]
            yield! [0 .. index]
            yield! indexSeq
        }

        let memoryBankArray = Array.ofList banks

        memoryBankArray.[index] <- 0
        for i in Seq.take value indexSeq do 
            memoryBankArray.[i] <- memoryBankArray.[i]+1
        
        List.ofArray memoryBankArray

    let findPrevious previousMemoryBanks updatedMemoryBanks =
        previousMemoryBanks 
            |> List.rev
            |> List.tryFindIndex ((=) updatedMemoryBanks)
        
    let rec loop count previousMemoryBanks currentMemoryBanks =
        let indexToReallocate, valueToReallocate =
            currentMemoryBanks
            |> List.indexed
            |> List.sortByDescending snd
            |> List.item 0
        let updatedMemoryBanks = reallocate indexToReallocate valueToReallocate currentMemoryBanks
        
        match findPrevious previousMemoryBanks updatedMemoryBanks with
        | Some foundIndex -> count-foundIndex-1
        | None -> loop (count+1) (updatedMemoryBanks :: previousMemoryBanks) updatedMemoryBanks
            
    loop 1 List.empty memoryBanks