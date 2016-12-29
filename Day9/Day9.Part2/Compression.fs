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
        
        let rec parseSection section pos acc = 
            if pos >= Array.length section then 
                acc
            else
                let currentChar = Array.get section pos
                if currentChar <> "(" then
                    parseSection  section (pos+1) (acc+1L)
                else
                    let markerArray = 
                        section
                        |> Array.skip (pos+1)
                        |> Array.takeWhile ((<>) ")")
                    let takeStart = 
                        markerArray
                        |> Array.length
                        |> (+) 2
                        |> (+) pos
                    let takeCount, repeat = 
                        markerArray
                        |> String.concat ""
                        |> toLower
                        |> split "x"
                        |> fun parts -> 
                            (
                                Array.get parts 0 |> Int32.Parse, 
                                Array.get parts 1 |> Int64.Parse
                            )
                    let markerArea = 
                        section
                        |> Array.skip takeStart
                        |> Array.take takeCount

                    let markerAreaCount = (parseSection  markerArea 0 0L) * repeat

                    parseSection section (takeStart+takeCount) (acc+markerAreaCount)
        
        parseSection input 0 0L

    let printLength (length:int64) =
        length
        |> printfn "Decompressed length is %i"