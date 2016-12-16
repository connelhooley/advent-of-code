module Types

type EncryptedRoom = {
    encryptedName: char[];
    sectorId: int;
    checksum: char[];
}

type DecryptedRoom = {
    name: string
    sectorId: int;
}