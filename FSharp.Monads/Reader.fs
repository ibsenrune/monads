namespace FSharp.Monads

type Reader<'r, 'a> = Reader of ('r -> 'a)

module Reader =
    let runReader (Reader r) env = r env

    let returnF a = Reader (fun _ -> a)

    let bind k m =
        Reader (fun r -> runReader (k (runReader m r)) r)

    let ask = Reader(id)

    let asks f =
        ask
        |> bind (f >> returnF)

type ReaderBuilder() =
    member this.Return(a) = Reader.returnF(a)
    member this.ReturnFrom reader = reader
    member this.Bind(m, k) = Reader.bind k m

[<AutoOpen>]
module ReaderComputationExpression =
    let reader = ReaderBuilder()