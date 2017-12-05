module Passphrase

let isValid (passphrase: string) = 
    
    let isMatch ((word1:string), (word2:string)) =
        let sort (s:string) = Array.sort (s.ToCharArray())
        sort word1 = sort word2
        
    let words = passphrase.Split()
    let distinctWords = Array.distinct words
    if distinctWords.Length <> words.Length then
        false
    else
        Array.allPairs words words
        |> Array.filter (fun (a, b) -> a <> b)
        |> Array.exists isMatch
        |> not