namespace Results

type Result = {
    robotNumber: int
    compareValue1: int
    compareValue2: int
}

type Results = Result list

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix )>]
module Results =
    let findComparison values results =
        let compare (needle1, needle2) haystack =
            haystack |> List.contains needle1 && haystack |> List.contains needle2
        results
        |> List.find (fun res -> compare values [res.compareValue1; res.compareValue2])
        |> fun res -> res.robotNumber