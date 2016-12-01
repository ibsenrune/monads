namespace FSharp.Monads

type Writer<'a> = Writer of 'a * string

module Writer =
  let runWriter (Writer(v, s)) = (v, s)
  let tell s = ((), s)
  let returnR v = Writer(v, "")
  let bind f (Writer(v,s)) =
    let (Writer(v',s')) = f v
    Writer(v', s + s')
  let map f (Writer(v, s)) = Writer(f v, s)
  
module WriterOperators =
  let (<!>) f w =  Writer.map f w
  let (>>=) w f = Writer.bind f w