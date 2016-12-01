module FSharp.Monads.Console

open FSharp.Monads
open FSharp.Monads.WriterOperators

let writeInput i = Writer (i, sprintf "%O" i)
let writeValue (Writer(v, s) as w) = w >>= writeInput

[<EntryPoint>]
let main argv =
    let value, log =
        4
        |> writeInput
        |> Writer.map ((*)4)
        |> writeValue
        |> Writer.runWriter

    System.Console.WriteLine(log)
    System.Console.WriteLine(value)

    0
