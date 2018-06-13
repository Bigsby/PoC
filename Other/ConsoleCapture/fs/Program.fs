// Learn more about F# at http://fsharp.org

open System

// [<EntryPoint>]
// let main =
while true  do
    let key = Console.ReadKey true
    match key.Key with
    | ConsoleKey.Q -> Environment.Exit 0
    | _ -> printfn "%c" key.KeyChar
    
    //0
