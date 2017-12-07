module Instructions

open System.IO

let readAll fileName =
    File.ReadAllLines(fileName)
    |> Array.map int

let perform (instructions: int[]) =
    let rec loop count pointer =
        if pointer < 0 || pointer > instructions.Length-1 then
            count
        else
            let currentValue = instructions.[pointer]
            let nextPointer = pointer + currentValue
            instructions.[pointer] <- currentValue+1 
            loop (count+1) nextPointer
    loop 0 0
 