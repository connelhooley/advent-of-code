module Types

type Room = {
    encryptedName: char[];
    sectorId: int;
    checksum: char[];
}