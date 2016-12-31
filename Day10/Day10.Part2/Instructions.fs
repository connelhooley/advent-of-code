namespace Instructions

open Instruction

type Instructions = Instruction list

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix )>]
module Instructions =

    open System
    open Helpers
    open Regex
    open Destination
    open Robot
    open OutputBin

    let parse (input:string[]) : Instructions =
        let mapInstruction line =
            match line with
            | Regex.IsInitialize (value, bot) -> 
                {
                    robotNumber = bot
                    task = Initialize value
                }
            | Regex.IsTransport (bot, lowType, lowNumber, highType, highNumber) ->
                let mapType = function
                    | "bot" -> RobotDestination
                    | "output" -> OutputBinDestination
                    | _ -> failwith "Invalid dest type"
                {
                    robotNumber = bot
                    task = Transport {
                        lowDestNumber = lowNumber
                        lowDestType = mapType lowType
                        highDestNumber = highNumber
                        highDestType = mapType highType
                    }
                }
            | _ -> failwith "Invalid input"

        input
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