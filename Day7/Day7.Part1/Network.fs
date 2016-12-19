module Network
    
open System
open Helpers

let private isAbba (groups:list<string>) =
    if (Seq.isEmpty groups) then
        false
    else if (groups.[2] = groups.[3]) then
        false
    else
        true

let private ipSupportsTls (input1, input2) =
    let abbaGroups1 = regex "(([a-z])([a-z])\3\2)" input1
    let abbaGroups2 = regex "(([a-z])([a-z])\3\2)" input2
    (isAbba abbaGroups1) || (isAbba abbaGroups2)

let private parseIp input =
    let groups = regex "([a-z]*)\[[a-z]*\]([a-z]*)" input
    (groups.[1], groups.[2])

let filterHypernets input =
    let hyperNetAbbaGroups = regex "[a-z]*\[(([a-z])([a-z])\3\2)\][a-z]*" input
    not(isAbba hyperNetAbbaGroups)

let countValid input =
    input
    |> Seq.filter filterHypernets
    |> Seq.map parseIp
    |> Seq.filter ipSupportsTls
    |> Seq.length