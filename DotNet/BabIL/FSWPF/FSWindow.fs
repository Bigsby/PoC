module FSControls

open System.Windows
open System.Windows.Controls
open WPFSharp

type ViewModel() =
    inherit ObservableObject()

    let _count = ref 0
    member this.ClickCommand = new DelegateCommand(fun _ -> this.Count <- this.Count + 1)

    member this.Count
        with get() = !_count
        and set(value) = 
            this.SetAndRaise(_count, value, "Count")

type MainWindow =
    static member Initialize(window: Window) =
        
        let vm = new ViewModel()
        window.DataContext <- vm
        let vm2 = new ViewModel()
        let dynamicText = window.GetElement<TextBlock>("dynamicText")
        dynamicText.Text <- "From classsss"

        window.GetElement<Button>("sameButton").Click
            |> Event.add(fun _ ->
                let count = vm.Count
                vm.Count <- count
                vm2.Count <- vm2.Count + 2
                dynamicText.Text <- sprintf "%s %d" dynamicText.Text count
            )