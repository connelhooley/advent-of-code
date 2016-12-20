open System
open System.IO

[<EntryPoint>]
let main argv = 
//    [
//        "abba[mnop]qrst";
//        "abcd[bddb]xyyx";
//        "aaaa[qwer]tyui";
//        "ioxxoj[asdfgh]zxcvbn";
//    ]
    File.ReadAllLines "input.txt"
    |> Network.countValid
    |> printfn "%i"
    ignore(Console.ReadLine())
    0 // return an integer exit code