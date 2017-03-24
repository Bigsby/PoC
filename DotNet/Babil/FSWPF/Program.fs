module App

open System
open System.Windows
open System.Windows.Controls
open System.Windows.Markup
open FSControls

[<STAThread>]
[<EntryPoint>]
let main (_) = 
    let application = Application.LoadComponent(new Uri("App.xaml", UriKind.Relative)) :?> Application

    application.Activated |> Event.add (fun _ -> 
        MainWindow.Initialize application.MainWindow
    )

    application.Run()