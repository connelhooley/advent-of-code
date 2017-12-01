module Decrypter

open System
open Types
open Helpers

let private getIntFromChar (c:char) = 
    ((int)c) - ((int)'a') + 1

let private getCharFromInt (i:int) = 
    (char)(i + ((int)'a') - 1)

let private move current shift = 
    (current+shift) % 26

let private shiftChar shift (c:char) = 
    let num = getIntFromChar c
    let newNum = move num shift
    if (newNum = 0) then c
    else getCharFromInt newNum

let private decryptName room = 
    room.encryptedName 
    |> Seq.map (shiftChar room.sectorId)
    |> String.Concat
    |> toLower

let private decryptRoom room = 
    {
        name = decryptName room;
        sectorId = room.sectorId;
     }

let decryptRooms rooms = 
    rooms
    |> Seq.map decryptRoom