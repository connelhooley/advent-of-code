module Helpers

open System
open System.Security.Cryptography

let toLower (x:string) =
    x.ToLower()

let hexToString (x:byte) =
    x.ToString("X2")

let parseInt (x:string) =
    let (isInt, value) = Int32.TryParse x
    match isInt with
    | true -> Some(value)
    | false -> None

let mergeStrings (x:seq<string>) =
    x |> Seq.fold (+) ""

let md5Hash (x:string) =
    let md5 = MD5.Create()
    let bytes = md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes(x))
    bytes
    |> Seq.map hexToString
    |> mergeStrings