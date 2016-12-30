namespace OutputBin

type OutputBin = int option

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix )>]
module OutputBin =
    
    let create = 
        None

    let sendMicrochip (value:int) (outputBin:OutputBin) : OutputBin =
        Some(value)
    
    let getMicrochip (outputBin:OutputBin) : int =
        outputBin|> Option.get