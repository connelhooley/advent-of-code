namespace Destination

type Destination<'a> = Map<int, 'a>

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix )>]
module Destination =
    
    let create: (Destination<_>) = Map.empty

    let modify (create: _) (number:int) (modifier:_->_) (destination:Destination<_>) : Destination<_> =
        match destination |> Map.tryFind number with
        | Some item ->
            destination
            |> Map.remove number
            |> Map.add number (modifier item)
        | None -> 
            destination
            |> Map.add number (modifier create)

    let tryFind (number:int) (destination:Destination<_>): _ =
        destination
        |> Map.tryFind number
    
    let find (number:int) (destination:Destination<_>): _ =
        destination
        |> Map.find number

    let items (destination:Destination<_>): _ =
        destination
        |> Map.toList
        |> List.map snd