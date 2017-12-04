module Passphrase

let isValid (passphrase: string) = 
    let isMatch ((word1:string), (word2:string)) =
        let string1 = 
            word1.ToCharArray()
            |> Array.sort
            |> Array.map string
            |> String.concat ""
        let string2 = 
            word2.ToCharArray()
            |> Array.sort
            |> Array.map string
            |> String.concat ""
        string1 = string2
        
    let words = passphrase.Split()
    let unique = 
        words 
        |> Array.distinct
    if unique.Length < words.Length then
        false
    else
        Array.allPairs words words
        |> Array.filter (fun (a, b) -> a <> b)
        |> Array.exists isMatch
        |> not