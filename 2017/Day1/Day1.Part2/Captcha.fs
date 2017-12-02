module Captcha

let parse (input:string) =     
    let rec circular = seq {
        yield! [0 .. input.Length-1]
        yield! circular
    }
    [0 .. input.Length-1]
    |> Seq.map (fun i -> 
        (
            input.[i], 
            input.[circular |> Seq.item (i + input.Length/2)])
        )
    |> Seq.filter (fun (a, b) -> a = b)
    |> Seq.map (fst >> (sprintf "%c") >> int)
    |> Seq.sum