module Passphrase

let isValid (passphrase: string) = 
    let sortWord (s:string) = Array.sort (s.ToCharArray())
        
    let words = passphrase.Split()
    let distinctWords = Array.distinct words
    if distinctWords.Length <> words.Length then
        false
    else
        Array.allPairs words words
        |> Array.filter (fun (word1, word2) -> word1 <> word2)
        |> Array.exists (fun (word1, word2) -> sortWord word1 = sortWord word2)
        |> not