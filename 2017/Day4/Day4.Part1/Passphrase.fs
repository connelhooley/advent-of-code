module Passphrase

let isValid (input: string) = 
    let words = 
        input.Split()
        |> Array.map(fun s -> s.Trim())

    let wordCount = 
        words 
        |> Array.length

    let uniqueWordCount = 
        words 
        |> Array.distinct
        |> Array.length
    
    wordCount = uniqueWordCount