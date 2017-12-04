module Passphrase

let isValid (input: string) = 
    let words = 
        input.Split()
        |> Array.map(fun s -> s.Trim())
    
    (words |> Array.length) = (words |> Array.distinct |> Array.length)