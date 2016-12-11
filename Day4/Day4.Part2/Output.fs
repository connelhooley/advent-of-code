module Output

open Types

let private decryptChar shift c = 
    c

let private decryptRoom room = 
    room.encryptedName
    |> Seq.map (decryptChar room.sectorId)

let decryptRooms rooms = 
    rooms
    |> Seq.map decryptRoom