namespace Screen

open Instruction

module ScreenModule = 
    let create width height = 
        Array2D.create height width false

    let perform screen instruction =
        
        let getRotationInput amount source = 
            let createInput length pixelIndexesToSwitchOn =
                let result = Array.create length false
                let turnOnPixel index = 
                     Array.set result index true
                Array.iter turnOnPixel pixelIndexesToSwitchOn
                result
            let shiftPixel amount length current =
                if amount = 0 then current
                else (current + amount) % length
            let length = Array.length source
            source
            |> Array.mapi (fun i p -> (i, p))
            |> Array.filter snd
            |> Array.map (fst >> shiftPixel amount length)
            |> createInput length

        match instruction with
        | Rectangle(width, height) -> 
            let input = Array2D.create height width true
            Array2D.blit input 0 0 screen 0 0 height width
        | RotateColumn(column, amount) ->
            let transpose array = seq {
                for i in array do
                    yield seq { yield i }
            }
            let height = screen 
                         |> Array2D.length1
            let input = screen.[*, column]
                        |> getRotationInput amount
                        |> transpose
                        |> array2D
            Array2D.blit input 0 0 screen 0 column height 1
        | RotateRow(row, amount) ->
            let width = screen
                        |> Array2D.length2
            let input = screen.[row, *]
                        |> getRotationInput amount
                        |> Array.singleton
                        |> array2D
            Array2D.blit input 0 0 screen row 0 1 width
        screen
    
    let print screen =
        let width = Array2D.length2 screen - 1
        let printPixel x y isPixelOn =
            printf "%s" (if isPixelOn then "#" else ".")
            if y = width then printfn ""
        screen
        |> Array2D.iteri printPixel
        let onPixelCount = screen 
                           |> Seq.cast
                           |> Seq.filter id 
                           |> Seq.length
        printfn ""
        printfn "%i pixels are turned on" onPixelCount