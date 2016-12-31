module Helpers

open System

let toString x =
    x.ToString ()

let isNotEmpty (x:string) =
    (not << String.IsNullOrEmpty) x