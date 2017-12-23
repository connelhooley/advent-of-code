module KnotHash

open System

let parseRound (fileContents:string) = 
    let lengthsInFile = fileContents.Trim().ToCharArray() |> Seq.map int
    let standardLengthSuffixes = [17; 31; 73; 47; 23]
    Seq.append lengthsInFile standardLengthSuffixes
        
let sparseHash round =

    let numbersSize = 255

    let rounds = seq {
        for _ in 1 .. 64 do 
            yield! round
    }

    let performLength (currentNumbers, currentPosition, currentSkipSize) length =     
        
        let numbers = Array.ofList currentNumbers
        
        let indexesToChange = 
            [currentPosition .. currentPosition+length]
            |> Seq.map (fun p -> p % numbersSize)
            
        let numbersToChange = 
            indexesToChange
            |> Seq.map (Array.get numbers)
            |> Seq.rev
        
        for (index, number) in Seq.zip indexesToChange numbersToChange  do
            numbers.[index] <- number
        
        let nextPosition = currentPosition+length+currentSkipSize
        let nextSkipSize = currentSkipSize+1
        let nextNumbers = List.ofArray numbers

        (nextNumbers, nextPosition, nextSkipSize)
            
    let result, _, _  =
        rounds
        |> Seq.fold performLength ([0 .. numbersSize], 0, 0)

    result
    
let denseHash numbers =
    numbers
    |> List.chunkBySize 16
    |> List.map (List.reduce (^^^))
    |> List.map (sprintf "%02X")
    |> List.reduce (+)