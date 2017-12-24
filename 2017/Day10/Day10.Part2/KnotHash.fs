module KnotHash

open System.Text

let parseRound (fileContents:string) = 
    let lengthsInFile = Encoding.ASCII.GetBytes(fileContents) |> Array.map int
    let standardLengthSuffixes = [17; 31; 73; 47; 23]
    Seq.append lengthsInFile standardLengthSuffixes
        
let sparseHash round =

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
            |> List.rev
            |> List.map (Array.get numbers)
        
        for (index, number) in Seq.zip indexesToChange numbersToChange  do
            numbers.[index] <- number
        
        let nextPosition = currentPosition+length+currentSkipSize
        let nextSkipSize = currentSkipSize+1
        let nextNumbers = List.ofArray numbers

        (nextNumbers, nextPosition, nextSkipSize)
        
    match Seq.fold performLength ([0 .. numbersSize-1], 0, 0) rounds with
    | result, _, _ -> result
    
let denseHash numbers =
    numbers
    |> List.chunkBySize 16
    |> List.map (List.reduce (^^^) >> sprintf "%02X")
    |> List.reduce (+)
    |> (fun s -> s.ToLower())