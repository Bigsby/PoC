let aFun input =
    if input
    then true
    else false


if (aFun true)
then printfn "True"
else printfn "False"

if (aFun false)
then printfn "True"
else printfn "False"

type ImperativeBuilder() =
    member this.Return x = x
    member this.For x = x
    member this.Zero = (fun () -> None)

let testSeq = seq {
    for i in 1..10 do
        yield i <> 20
} 

let result = testSeq |> Seq.fold (fun i state -> i && state) true

printfn "Result: %b" result