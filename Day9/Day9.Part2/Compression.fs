namespace Compression

type Marker = {
    size:int
    repeat:int
}

type CompressionItem =
    | Marker of Marker
    | Value

type ActiveMarker = {
    remainingSize:int
    repeat:int
}

type CompressionState = {
    length: int
    activeMarkers: list<ActiveMarker>
}

module CompressionModule =

    open System
    open Helpers
    
    let parseInput input =
        let parseRegexMatchGroups groups =
            if (groups |> List.item 3 |> String.IsNullOrEmpty) then
                Marker {
                    size   = groups |> List.item 1 |> Int32.Parse
                    repeat = groups |> List.item 2 |> Int32.Parse
                }
            else
                Value
        input
        |> regexGroups @"\((\d+)x(\d+)\)|(\w)"
        |> Seq.map parseRegexMatchGroups

    let getDecompressedLength input =

        let decompressItem state item =             
            match item with
            | Marker(marker) -> 
                let newActiveMarker = {
                    remainingSize = marker.size
                    repeat = marker.repeat
                }
                let newActiveMarkers = newActiveMarker :: state.activeMarkers
                { state with activeMarkers = newActiveMarkers }
            | Value ->
                let reduceRemainingSize activeMarker =
                    { activeMarker with remainingSize = activeMarker.remainingSize-1 }
                let isMarkerNotExpired activeMarker =
                    activeMarker.remainingSize > 0
                let newLength = 
                    state.activeMarkers
                        |> List.map (fun am -> am.repeat)
                        |> List.fold (*) 1
                        |> (+) state.length
                let newActiveMarkers = 
                    state.activeMarkers
                    |> List.map reduceRemainingSize
                    |> List.filter isMarkerNotExpired
                { state with length = newLength; activeMarkers = newActiveMarkers }
                
        let startingState = {
            length = 0
            activeMarkers = []
        }
        input
        |> Seq.fold decompressItem startingState
        |> fun state -> state.length

    let printLength length =
        length
        |> printfn "Decompressed length is %i"