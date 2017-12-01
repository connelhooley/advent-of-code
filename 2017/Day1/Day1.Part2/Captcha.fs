module Captcha

let parse (input:string) =     
    let rec circular = seq {
        yield! [0..input.Length-1]
        yield! circular
    }
    [0 .. input.Length-1]
    |> Seq.map (fun i -> (Seq.item i circular, Seq.item (i + input.Length/2) circular))
    |> Seq.map (fun (a, b) -> (input.[a], input.[b]))
    |> Seq.filter (fun (a, b) -> a = b)
    |> Seq.map fst
    |> Seq.map (sprintf "%c")
    |> Seq.map int
    |> Seq.sum