module WPFSharp

open System
open System.ComponentModel
open System.Windows
open System.Windows.Input

[<AbstractClass>]
type ObservableObject() =
    let propertyChangedEvent = new Event<PropertyChangedEventHandler,PropertyChangedEventArgs>()

    interface INotifyPropertyChanged with
        [<CLIEvent>]
        member this.PropertyChanged = propertyChangedEvent.Publish

    member this.RaiseProperytChanged propertyName = 
        propertyChangedEvent.Trigger(this, PropertyChangedEventArgs(propertyName))
    
    member this.SetAndRaise (field: Ref<'T>, value: 'T, propertyName) =
        if !field <> value then
            field := value
            this.RaiseProperytChanged(propertyName)

type FrameworkElement with
    member this.GetElement<'T when 'T:> FrameworkElement>(name) =
        let element = this.FindName(name)
        if (element :? 'T) then element :?> 'T
        else Unchecked.defaultof<'T>

    member this.SafeElement<'T when 'T:> FrameworkElement and 'T:null and 'T: equality>(name, action) =
        let element = this.GetElement<'T>(name)
        if element = null
        then ()
        else action element

type DelegateCommand(action) =
    let canExecuteChanged = new Event<EventHandler,EventArgs>()
    interface ICommand with
        [<CLIEvent>]
        member this.CanExecuteChanged: IEvent<EventHandler,EventArgs> = 
            canExecuteChanged.Publish

        member this.CanExecute(param: Object) = true
        member this.Execute(param: Object) =
            action()
