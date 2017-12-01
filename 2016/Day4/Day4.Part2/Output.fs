module Output

open Types
open Helpers

let private filterRooms room = 
    room.name
    |> contains "pole"

let private getSectorId room =
    room.sectorId

let getPoleSectorIds rooms = 
    rooms
    |> Seq.filter filterRooms
    |> Seq.map getSectorId