module FSharp.Monads.Console

open FSharp.Monads
open FSharp.Monads.WriterOperators

let writerSample () =
    let write i = Writer (i, sprintf "%O" i)

    let value, log =
        4
        |> write
        |> Writer.map ((*)4)
        >>= write
        |> Writer.runWriter

    System.Console.WriteLine(log)
    System.Console.WriteLine()
    System.Console.WriteLine(value)

type ConnectionString = ConnectionString of string
type Configuration = { ConnectionString : ConnectionString }
let readerSample () =
    let readFromDb connnectionString = 5
    let program = 
        reader {
            let x = 4
            let! conn = Reader.ask
            let y = readFromDb conn
            return x + y
        }
    program
    
[<EntryPoint>]
let main argv =
    let configuration = { ConnectionString = ConnectionString "foo" }
    let program = readerSample ()
    let result = Reader.runReader program configuration

    0
