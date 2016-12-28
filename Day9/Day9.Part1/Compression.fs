namespace Compression

module CompressionModule =

    open System
    open Helpers
    
    let parseInput input =
        input
        |> trim
        |> toCharArray
        |> Array.map toString

    let decompress input =
        
        let parseMarker parts = 
            (
                Array.get parts 0 |> Int32.Parse,
                Array.get parts 1 |> Int32.Parse
            )

        let rec loop pos output = 
            if pos >= Array.length input then 
                output
            else
                let currentChar = Array.get input pos
                if currentChar <> "(" then
                    loop (pos+1) (output+currentChar)
                else
                    let markerArray = 
                        input
                        |> Array.skip (pos+1)
                        |> Array.takeWhile ((<>) ")")
                    let markerLength = 
                        markerArray
                        |> Array.length
                    let takeCount, repeatCount = 
                        markerArray
                        |> String.concat ""
                        |> toLower
                        |> split "x"
                        |> parseMarker
                    let takeStart = pos + 1 + markerLength + 1
                    let newOutput = 
                        input
                        |> Array.skip takeStart
                        |> Array.take takeCount
                        |> String.concat ""
                        |> String.replicate repeatCount
                    loop (takeStart+takeCount) (output+newOutput)
        
        loop 0 ""

    let printOutput output =
        output
        |> String.length
        |> printfn "Decompressed length is %i"