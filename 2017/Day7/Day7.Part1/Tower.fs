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
        let parseChildren (input:string) =
            input.Split(',')
            |> Array.map (fun s -> s.Trim())
            |> Array.filter (not << System.String.IsNullOrWhiteSpace)
            |> List.ofArray            
        let _match = Regex.Match(line, "([a-z]+) \((\d*)\)[ ->]*([a-z ,]*)")
        {
            Name = _match.Groups.[1].Value
            Weight = _match.Groups.[2].Value |> int
            Children = _match.Groups.[3].Value |> parseChildren
        }

    fileName
    |> File.ReadAllLines
    |> Array.map matchLine
    |> List.ofArray

let findRoot programs = 
    
    let isLeafNode programs program =
        programs
        |> List.exists (fun p -> p.Children |> List.contains program.Name)
        |> not

    let rec filter programs =
        let filtered = programs |> List.filter (isLeafNode programs)
        if filtered.Length = 1 
        then
            filtered.Head.Name
        else 
            filter filtered
    
    filter programs