module Tower

open System.IO
open System.Text.RegularExpressions

type Program = {
    Name: string
    Weight: int
    Children: string list
}

let read fileName =
    let matchLine line =
        let _match = Regex.Match(line, "([a-z]+) \((\d*)\)[ ->]*([a-z ,]*)")
        {
            Name = 
                _match.Groups.[1].Value
            Weight = 
                _match.Groups.[2].Value |> int
            Children = 
                if System.String.IsNullOrWhiteSpace _match.Groups.[3].Value then
                    List.empty
                else
                    List.ofArray (_match.Groups.[3].Value.Split ", ")
        }

    fileName
    |> File.ReadAllLines
    |> Array.map matchLine
    |> List.ofArray


let findDesiredChildWeight programs =

    let findRootName programs = 
        let hasNoParent program = 
            programs
            |> List.exists (fun p -> p.Children |> List.contains program.Name)
            |> not
        let root = 
            programs 
            |> List.find hasNoParent
        root.Name

    let findProgram name = 
        programs 
        |> List.find (fun program -> program.Name = name)
        
    let rec findProgramNameWeight name =
        let program = findProgram name

        if program.Children |> List.isEmpty then
            program.Weight
        else
            program.Children
            |> List.map findProgramNameWeight
            |> List.sum
            |> ((+) program.Weight)
    
    let rec findOddProgramName name = 
        let isChildOld child =
            let isOdd = 
                child.Children 
                |> List.map findProgramNameWeight
                |> List.distinct
                |> List.length <> 1

            match isOdd with
            | true -> Some(child.Name)
            | false -> None

        let program = findProgram name

        let oddChild =
            program.Children
            |> List.tryPick (findProgram >> isChildOld)

        match oddChild with
        | Some(childName) -> findOddProgramName childName
        | None -> name
        
    let oddProgram = 
        programs
        |> findRootName
        |> findOddProgramName
        |> findProgram

    let oddProgramChildNameTotalWeights =
        oddProgram.Children
        |> List.map (fun childName -> (childName, findProgramNameWeight childName))
        
    let (oddChildName, oddChildTotalWeight) =
        oddProgramChildNameTotalWeights
        |> List.groupBy snd
        |> List.find (fun group ->
            group
            |> snd
            |> List.length = 1)
        |> snd
        |> List.head

    let oddChild = findProgram oddChildName

    let smallestOddChildTotalWeight = 
        oddProgramChildNameTotalWeights
        |> List.map snd
        |> List.min

    let largestOddChildTotalWeight = 
        oddProgramChildNameTotalWeights
        |> List.map snd
        |> List.max
    
    let difference = largestOddChildTotalWeight - smallestOddChildTotalWeight
    
    if oddChildTotalWeight = smallestOddChildTotalWeight then
        oddChild.Weight + difference
    else
        oddChild.Weight - difference