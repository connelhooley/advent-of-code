open System
open System.IO

[<EntryPoint>]
let main argv =
//    @"
//eedadn
//drvtee
//eandsr
//raavrd
//atevrs
//tsrnev
//sdttsa
//rasrtv
//nssdts
//ntnada
//svetve
//tesnvt
//vntsnd
//vrdear
//dvrsen
//enarar"
    File.ReadAllText "input.txt"
    |> Input.parse
    |> Output.decode
    |> printfn "%s"
    ignore(Console.ReadLine())
    0 // return an integer exit code