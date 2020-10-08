#r @"C:\Users\gvida\.nuget\packages\fparsec\1.1.1\lib\net45\FParsecCS.dll"
#r @"C:\Users\gvida\.nuget\packages\fparsec\1.1.1\lib\net45\FParsec.dll"

open FParsec

let test p str =
    match run p str with
    | Success(result, _, _) -> printfn "Success: %A" result
    | Failure(errorMsg, _, _) -> printfn "Failure: %s" errorMsg

// Declaration of Discriminative Unions or Sum tyepes that are needed for determining the sound
type MeasureFraction = Half | Quarter | Eighth | Sixteenth | Thirtyseconth
type Length = { fraction: MeasureFraction; extended: bool}
type Note = A | ASharp | B | C | CSharp | D | DSharp | E | F | FSharp | G | GSharp
type Octave = One | Two | Three
type Sound = Rest | Tone of note: Note * octave: Octave
type Token = { length: Length; sound: Sound}

let aspiration = "32.#d3"

//First parser to parse the MeasureFraction
let pmeasurefraction =
    (stringReturn "2" Half)
    <|> (stringReturn "4" Quarter)
    <|> (stringReturn "8" Eighth)
    <|> (stringReturn "16" Sixteenth)
    <|> (stringReturn "32" Thirtyseconth)

//Another parser to determine whether there is an extension or not
let pextendedparser = (stringReturn "." true) <|> (stringReturn "" false)

//We are now looking to parse a length which is requiring us to use multiple parses and combine the result to a single value
let plength = 
    pipe2
        pmeasurefraction
        pextendedparser
        (fun t e -> {fraction = t; extended = e})

//We are creating a parser for non sharpable notes
let pnotsharpablenote = anyOf "be" |>> (function
                             | 'b' -> B
                             | 'e' -> E
                             | unknown -> sprintf "Unknown note %c" unknown |> failwith)

//Sharpable character parser
let psharp = (stringReturn "#" true) <|> (stringReturn "" false)

let psharpnote = pipe2
                    psharp
                    (anyOf "acdfg")
                    (fun isSharp note ->
                        match (isSharp, note) with
                            | (false, 'a') -> A
                            | (true, 'a') -> ASharp
                            | (false, 'c') -> C
                            | (true, 'c') -> CSharp
                            | (false, 'd') -> D
                            | (true, 'd') -> DSharp
                            | (false, 'f') -> F
                            | (true, 'f') -> FSharp
                            | (false, 'g') -> G
                            | (true, 'g') -> GSharp
                            | (_, unknown) -> sprintf "Unknown note %c" unknown |> failwith)

let pnote = pnotsharpablenote <|> psharpnote

//Parse Octave
let poctave = anyOf "123" |>> (function
                | '1' -> One
                | '2' -> Two
                | '3' -> Three
                | unknown -> sprintf "Unknown octave %c" unknown |> failwith)


//Now the second step of the way is to start combining the parsers into larger ones. Since we have all the individual pieces, we need them to work as one to be able to 
//determine something more difficult as a sound
//First step is parsing the tone. We have already defined what it is. It is a note and an octave
let ptone = pipe2 pnote poctave (fun n o -> Tone(note = n, octave = o))

//Rest value
let prest = stringReturn "-" Rest

//Final parser for a full token
let ptoken = pipe2 plength (prest <|> ptone) (fun l t -> {length = l; sound = t})

//Final parser for the full composition. This parser basically is a list of tokens seperated by space
let pscore = sepBy ptoken (pstring " ")

test pscore "2- 16a1 16- 16a1 16- 8a1 16- 4a2 16g2 16- 2g2 16- 4- 8- 16g2 16- 16g2 16- 16g2 8g2 16-"

test ptoken aspiration

test ptone "#d3"

test poctave "2"

test pnote "#a"

test plength aspiration

test pmeasurefraction aspiration
