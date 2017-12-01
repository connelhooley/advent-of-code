namespace Robot

type Robot = int list

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix )>]
module Robot =
    
    let create : Robot = 
        List.empty

    let getMicrochips (robot:Robot) : min:int * max:int =
        if robot |> List.length < 2 then
            failwith "Robot does not hold 2 microchips yet"
        else
            (
                robot |> List.min,
                robot |> List.max
            )

    let sendMicrochip (value:int) (robot:Robot) : Robot =
        if robot |> List.length > 1 then
            failwith "Robot can only hold 2 microchips"
        else
            value :: robot
    
    let clearMicrochips (robot:Robot) : Robot =
        List.empty
    
    let isReady (robot:Robot) : bool =
        robot |> List.length > 1
