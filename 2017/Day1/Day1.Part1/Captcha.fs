module Captcha

let parse (input:string) = 
    seq {
        yield! input
        yield input.[0]
    }
    |> Seq.pairwise
    |> Seq.filter (fun (a, b) -> a = b)
    |> Seq.map fst
    |> Seq.map (sprintf "%c")
    |> Seq.map int
    |> Seq.sum