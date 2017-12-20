module KnotHash

open System.IO

type State = {
    Numbers: int seq
    Position: int
    SkipSize: int
}

let parse fileName = 
    File.ReadAllText(fileName).Split(',')
    |> Seq.ofArray
    |> Seq.map int

let perform numbers lengths =
    let rec indexes = seq {
        yield! numbers
        yield! indexes
    }

    let performLength state length =     
        let numberArray = 
            state.Numbers
            |> Array.ofSeq

        let indexesToChange = 
            indexes
            |> Seq.skip state.Position
            |> Seq.take length
            |> Array.ofSeq
            
        let numbersToChange = 
            indexesToChange
            |> Seq.map (Array.get numberArray)
            |> Seq.rev
            |> Array.ofSeq

        Seq.zip indexesToChange numbersToChange
        |> Seq.iter(fun (index, number) -> numberArray.[index] <- number)
        
        {
            Numbers = numberArray |> Seq.ofArray
            Position = state.Position+length+state.SkipSize
            SkipSize = state.SkipSize+1
        }
        
    let inital = {
        Numbers = numbers
        Position = 0
        SkipSize = 0
    }
    let result =
        lengths
        |> Seq.fold performLength inital
    
    result.Numbers

let multiplyFirstTwoNumbers numbers =
    let first = Seq.item 0 numbers
    let second = Seq.item 1 numbers
    first * second