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

let findRoot programs = 
    let hasNoParent program = 
        programs
        |> List.exists (fun p -> p.Children |> List.contains program.Name)
        |> not
    let root = 
        programs 
        |> List.find hasNoParent
    root.Name