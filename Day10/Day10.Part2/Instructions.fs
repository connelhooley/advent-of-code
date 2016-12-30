namespace Instructions

open Instruction

type Instructions = Instruction list

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix )>]
module Instructions =

    open System
    open Helpers
    open Destination
    open Robot
    open OutputBin

    let parse (input:string) : Instructions =
        let mapInstruction groups =
            let (|IsInitialize|_|) input =
                let isValid = 
                    input
                    |> List.take 2
                    |> List.forall isNotEmpty
                if isValid then
                    Some (
                        input |> List.item 0 |> Int32.Parse, 
                        input |> List.item 1 |> Int32.Parse
                    )
                else 
                    None
            
            let (|IsTransport|_|) input =
                let isValid = 
                    input
                    |> List.skip 2
                    |> List.take 5
                    |> List.forall isNotEmpty
                
                if isValid then 
                    Some (
                        input |> List.item 2 |> Int32.Parse,
                        input |> List.item 3,
                        input |> List.item 4 |> Int32.Parse,
                        input |> List.item 5,
                        input |> List.item 6 |> Int32.Parse
                    )
                else 
                    None

            match groups with
            | IsInitialize (value, bot) -> 
                {
                    robotNumber = bot
                    task = Initialize value
                }
            | IsTransport (bot, lowType, lowNumber, highType, highNumber) ->
                let lowDest =
                    match lowType with
                    | "bot" -> RobotDestination
                    | "output" -> OutputBinDestination
                    | _ -> failwith "Invalid low dest type"
                let highDest =
                    match highType with
                    | "bot" -> RobotDestination
                    | "output" -> OutputBinDestination
                    | _ -> failwith "Invalid high dest type"
                {
                    robotNumber = bot
                    task = Transport {
                        lowDestNumber = lowNumber
                        lowDestType = lowDest
                        highDestNumber = highNumber
                        highDestType = highDest
                    }
                }
            | _ -> failwith "Invalid input"

        input
        |> regexGroups @"value (\d+) goes to bot (\d+)|bot (\d+) gives low to (bot|output) (\d+) and high to (bot|output) (\d+)"
        |> Seq.map mapInstruction
        |> List.ofSeq

    let perform (instructions:Instructions) : InstructionState =
        
        let rec loop ((robots, outputBins) as state) instructions =
            
            if instructions |> List.isEmpty then
                state
            else
                let instruction = 
                    instructions
                    |> List.find (Instruction.isReady robots)
            
                let updatedState = 
                    instruction
                    |> Instruction.perform (robots, outputBins)

                let updatedInstructions =
                    instructions
                    |> List.filter ((<>) instruction)

                loop updatedState updatedInstructions

        loop (Destination.create, Destination.create) instructions