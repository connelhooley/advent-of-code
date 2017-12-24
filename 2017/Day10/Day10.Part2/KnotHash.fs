module KnotHash

let parseRound (fileContents: string) = 
    let lengthsInFile = fileContents.ToCharArray() |> Seq.map int
    let standardLengthSuffixes = [17; 31; 73; 47; 23]
    Seq.append lengthsInFile standardLengthSuffixes
        
let sparseHash (round: int seq): int list =

    let numbersSize = 256
    
    let rounds = 
        Seq.replicate 64 round
        |> Seq.collect id
    
    let performLength (currentNumbers, currentPosition, currentSkipSize) length =
            
        let numbers = Array.ofList currentNumbers
       
        let indexesToChange = 
            [currentPosition .. currentPosition+length-1]
            |> List.map (fun p -> p % (numbersSize))
        
        let numbersToChange = 
            indexesToChange
            |> List.map (Array.get numbers)
            |> List.rev
        
        for (index, number) in Seq.zip indexesToChange numbersToChange  do
            numbers.[index] <- number
        
        let nextPosition = currentPosition+length+currentSkipSize
        let nextSkipSize = currentSkipSize+1
        let nextNumbers = List.ofArray numbers

        (nextNumbers, nextPosition, nextSkipSize)
        
    match Seq.fold performLength ([0 .. numbersSize-1], 0, 0) rounds with
    | result, _, _ -> result
    
let denseHash (numbers: int list): string =
    numbers
    |> List.chunkBySize 16
    |> List.map (List.reduce (^^^) >> sprintf "%02X")
    |> List.reduce (+)
    |> (fun s -> s.ToLower())