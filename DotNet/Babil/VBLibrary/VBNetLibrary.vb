Imports System
Public Class VBNetLibrary

    Public Shared Name As String = "VB.Net Library"

    Public Shared Sub WriteToConsole()
        Console.WriteLine($"Written from {Name}")
    End Sub

    Public Shared Sub WriteThisToConsole(ByVal what As String)
        Console.WriteLine($"You told {Name} to write: {what}")
    End Sub

    Public Shared Function WhoAmI() As String
        WhoAmI = Name
    End Function

End Class