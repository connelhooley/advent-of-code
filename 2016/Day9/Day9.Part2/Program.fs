open Compression

open System
open System.IO

[<EntryPoint>]
let main argv =
    File.ReadAllText "input.txt"
    |> CompressionModule.parseInput
    |> CompressionModule.getDecompressedLength
    |> CompressionModule.printLength
    ignore(Console.ReadLine())
    0 // return an integer exit code
