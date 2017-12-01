module Output

open Types
open Helpers

let parse (numbers:seq<int>) =  
    numbers
    |> Seq.map toString
    |> (String.concat "")