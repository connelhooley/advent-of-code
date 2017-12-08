module Reallocation

open System.IO
open System

type private State = {
    History: int[][]

}

let read fileName =
    File.ReadAllText(fileName).Split()
    |> Array.filter (not << String.IsNullOrWhiteSpace)
    |> Array.map int

let countReallocates memoryBanks = 
    let rec loop count previous =
        let indexToReallocate =
            memoryBanks
            |> Array.mapi (fun i x -> (i, x))
            |> Array.sortByDescending snd
            |> Array.item 0
            |> fst
        let valueToReallocate = memoryBanks.[indexToReallocate]
    
        let rec indexSeq = seq {
            yield! [indexToReallocate+1 .. memoryBanks.Length-1]
            yield! [0 .. indexToReallocate]
            yield! indexSeq
        }

        memoryBanks.[indexToReallocate] <- 0
        for i in Seq.take valueToReallocate indexSeq do 
            memoryBanks.[i] <- memoryBanks.[i]+1
                
        let updatedMemoryBanks = List.ofArray memoryBanks

        if List.contains updatedMemoryBanks previous then
            count
        else
            loop (count+1) (updatedMemoryBanks :: previous)
    
    loop 1 List.empty