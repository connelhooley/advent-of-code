module Output

open Types


let private mapSectorId room =
    room.sectorId

let sumSectorIds rooms = 
    rooms
    |> Seq.map mapSectorId
    |> Seq.sum