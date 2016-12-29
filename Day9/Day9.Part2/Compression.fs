namespace Compression

module CompressionModule =

    open System
    open Helpers
    
    let parseInput input =
        input
        |> trim
        |> toCharArray
        |> Array.map toString

    let getDecompressedLength input =
        
        let parseMarker parts = 
            (
                Array.get parts 0 |> Int32.Parse,
                Array.get parts 1 |> Int64.Parse
            )

        let rec parseSection  input pos (output:int64) = 
            if pos >= Array.length input then 
                output
            else
                let currentChar = Array.get input pos
                if currentChar <> "(" then
                    parseSection  input (pos+1) (output+1L)
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
                    let markerArea = 
                        input
                        |> Array.skip takeStart
                        |> Array.take takeCount

                    let markerAreaCount = (parseSection  markerArea 0 0L) * repeatCount

                    parseSection input (takeStart+takeCount) (output+markerAreaCount)
        
        parseSection input 0 0L

    let printLength (length:int64) =
        length
        |> printfn "Decompressed length is %i"