module Grid

open System.IO
open System

let read fileName = 
    fileName
    |> File.ReadAllLines
    |> Array.map (fun line -> line.Split() |> Array.map int)