namespace FSharp.Monads

type State<'s, 'v> = State of ('s -> 'v * 's)

module State =
    let returnM v = State (fun s -> v, s)

    let bind (State g) f =
        (fun s -> 
            let (v, s') = g s
            let (State f') = f v
            f' s') |> State

type StateBuilder<'s>() =
    member this.Return v = State.returnM v
    member this.Bind(m, f) = State.bind m f
    member this.ReturnFrom s = s

[<AutoOpen>]
module StateBuilderExpression =
    let withState<'s> = StateBuilder<'s>()
    let getState = State(fun s -> s, s)
    let putState v = State(fun _ -> (), v)
    let runState (State f) initialState = f initialState