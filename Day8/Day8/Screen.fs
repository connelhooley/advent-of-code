namespace Screen

open Instruction

module ScreenModule = 
    let create width height = 
        Array2D.create height width false

    let perform screen instruction =
        
        let getRotationInput amount source = 
            let length = Array.length source
            let createInput pixelIndexesToSwitchOn =
                let result = Array.create length false
                let turnOnPixel index = 
                     Array.set result index true
                Array.iter turnOnPixel pixelIndexesToSwitchOn
                result
            let shiftPixel current =
                if amount = 0 then current
                else (current + amount) % length
            source
            |> Array.mapi (fun index pixel -> index, pixel)
            |> Array.filter snd
            |> Array.map (fst >> shiftPixel)
            |> createInput

        match instruction with
        | Rectangle(width, height) -> 
            let input = Array2D.create height width true
            Array2D.blit input 0 0 screen 0 0 height width
        | RotateColumn(column, amount) ->
            let input = screen.[*, column]
                        |> getRotationInput amount
                        |> Array.map Array.singleton
                        |> array2D
            let height = screen
                         |> Array2D.length1
            Array2D.blit input 0 0 screen 0 column height 1
        | RotateRow(row, amount) ->
            let input = screen.[row, *]
                        |> getRotationInput amount
                        |> Array.singleton
                        |> array2D
            let width = screen
                        |> Array2D.length2
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