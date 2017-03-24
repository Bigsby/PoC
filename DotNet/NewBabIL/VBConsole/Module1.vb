Imports CSLibrary
Imports FSLibrary
Imports VBLibrary
Imports System

Module Module1

    Sub Main()
        Console.WriteLine("VB.Net Console App here...")
        Console.WriteLine("Calling libraries...")
        Console.WriteLine()

        Console.WriteLine($"Name: {CSharpLibrary.Name}")
        CSharpLibrary.WriteToConsole()
        CSharpLibrary.WriteThisToConsole("VB.Net Console App here!")
        Console.WriteLine("Who are you? " + CSharpLibrary.WhoAmI())
        Console.WriteLine()

        Console.WriteLine($"Name: {FSharpLibrary.Name}")
        FSharpLibrary.WriteToConsole()
        FSharpLibrary.WriteThisToConsole("VB.Net Console App here!")
        Console.WriteLine("Who are you? " + FSharpLibrary.WhoAmI())
        Console.WriteLine()

        Console.WriteLine($"Name: {VBNetLibrary.Name}")
        VBNetLibrary.WriteToConsole()
        VBNetLibrary.WriteThisToConsole("VB.Net Console App here!")
        Console.WriteLine("Who are you? " + VBNetLibrary.WhoAmI())


        Console.WriteLine("Done!")
        Console.ReadLine()
    End Sub

End Module