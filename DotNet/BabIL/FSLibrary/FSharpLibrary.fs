namespace FSLibrary

type FSharpLibrary = 

    static member Name = "F# Library"

    // brackets needed to be a method for the outside world
    static member WriteToConsole() = 
        printfn "Written from %s" FSharpLibrary.Name

    static member WriteThisToConsole what =
        printfn "You told %s to write: %s" FSharpLibrary.Name what

    static member WhoAmI() = FSharpLibrary.Name