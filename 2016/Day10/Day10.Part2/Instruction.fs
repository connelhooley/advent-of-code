namespace Instruction
    
open Destination
open Robot
open OutputBin

type DestinationType =
    | RobotDestination
    | OutputBinDestination
    
type Transport = {
    lowDestNumber: int
    lowDestType: DestinationType
    highDestNumber: int
    highDestType: DestinationType
}

type Task =
    | Initialize of int
    | Transport of Transport

type Instruction = {
    robotNumber: int
    task: Task
}

type InstructionState = Robot Destination * OutputBin Destination

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix )>]
module Instruction =
    
    let isReady (robots:Robot Destination) (instruction:Instruction) : bool = 
        match instruction.task with
        | Initialize _ -> true
        | Transport task -> 
            match robots |> Destination.tryFind instruction.robotNumber with
            | Some robot -> robot |> Robot.isReady
            | None -> false

    let perform ((robots, outputBins) as state:InstructionState) (instruction:Instruction) : InstructionState =
        let modifyRobots = Destination.modify Robot.create
        let modifyOutputbins = Destination.modify OutputBin.create
        match instruction.task with
                | Initialize microchip ->
                    (
                        robots |> modifyRobots instruction.robotNumber (Robot.sendMicrochip microchip),
                        outputBins
                    )
                | Transport task ->
                    let low, high = 
                        robots
                        |> Destination.find instruction.robotNumber
                        |> Robot.getMicrochips

                    let clear sourceNumber (robots, outputBins) =
                        (
                            robots |> modifyRobots sourceNumber (Robot.clearMicrochips),
                            outputBins
                        )
                    let performTransport destType destNumber microchip (robots, outputBins) =
                        match destType with
                        | RobotDestination -> 
                            (
                                robots |> modifyRobots destNumber (Robot.sendMicrochip microchip),
                                outputBins
                            )
                        | OutputBinDestination -> 
                            (
                                robots,
                                outputBins |> modifyOutputbins destNumber (OutputBin.sendMicrochip microchip)
                            )

                    state
                    |> performTransport task.lowDestType task.lowDestNumber low
                    |> performTransport task.highDestType task.highDestNumber high
                    |> clear instruction.robotNumber
