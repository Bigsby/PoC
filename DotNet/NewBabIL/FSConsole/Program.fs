open System
open CSLibrary
open FSLibrary
open VBLibrary


[<EntryPoint>]
let main argv = 
    printfn "F# Console App here..."
    printfn "Calling libraries..."
    printfn ""

    printfn "Name: %s" CSharpLibrary.Name
    // brackets needed on invoke
    CSharpLibrary.WriteToConsole() |> ignore
    CSharpLibrary.WriteThisToConsole "F# Console App here!"
    let csWhoAmI = CSharpLibrary.WhoAmI()
    printfn "Who are you? %s" csWhoAmI
    printfn ""

    printfn "Name: %s" FSharpLibrary.Name
    FSharpLibrary.WriteToConsole() |> ignore
    FSharpLibrary.WriteThisToConsole "F# Console App here!"
    let fsWhoAmI = FSharpLibrary.WhoAmI()
    printfn "Who are you? %s" fsWhoAmI
    printfn ""

    printfn "Name: %s" VBNetLibrary.Name
    VBNetLibrary.WriteToConsole() |> ignore
    VBNetLibrary.WriteThisToConsole "F# Console App here!"
    let vbWhoAmI = VBNetLibrary.WhoAmI()
    printfn "Who are you? %s" vbWhoAmI
    printfn ""

    printfn "Done!"
    Console.ReadLine() |> ignore

    0