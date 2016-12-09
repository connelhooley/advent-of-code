module Output

open Types

let private isLargerThan sideA sideB sideC = 
    (sideA + sideB) > sideC

let isValid (side1, side2, side3) = 
    isLargerThan side1 side2 side3 &&
    isLargerThan side2 side3 side1 &&
    isLargerThan side3 side1 side2

let countValid triangles = 
    triangles
    |> Seq.filter isValid
    |> Seq.length