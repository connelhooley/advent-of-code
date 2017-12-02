module Captcha

let parse (input:string) = 
    seq {
        yield! input
        yield input.[0]
    }
    |> Seq.pairwise
    |> Seq.filter (fun (a, b) -> a = b)
    |> Seq.map (fst >> (sprintf "%c") >> int)
    |> Seq.sum