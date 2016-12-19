module Password

open System.Security.Cryptography

let private toLower (x:string) =
    x.ToLower()

let private hexToString (x:byte) =
    x.ToString("X2")

let private getResult (currentResult:string) (hash:string) = 
    if(hash.StartsWith "00000") then
        currentResult + hash.Substring(5, 1)
    else 
        currentResult

let private getHash doorId number = 
    let value = sprintf "%s%i" doorId number
    let md5 = MD5.Create()
    let bytes = md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes(value))
    bytes
    |> Seq.map hexToString
    |> Seq.fold (+) ""

let calculate doorId =
    let rec loop number result =
        let hash = getHash doorId number
        let newResult = getResult result hash
        if (newResult.Length = 9) then
            result
        else      
            if(newResult <> result) then printfn "%s" newResult
            loop (number + 1) newResult
    loop 0 "" |> toLower