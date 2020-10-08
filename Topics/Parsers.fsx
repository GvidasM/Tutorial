#r @"C:\Users\gvida\.nuget\packages\fparsec\1.1.1\lib\net45\FParsecCS.dll"
#r @"C:\Users\gvida\.nuget\packages\fparsec\1.1.1\lib\net45\FParsec.dll"

open FParsec

let parsesA = pchar 'a'
run parsesA "abcdefg"

run (many anyChar) "abcde fg"

run (anyChar) "abcdefg" 
    |> sprintf "%+A"
run (pchar 'a') "abcdefg" 
    |> sprintf "%+A"

run (stringReturn "foo" 7) "foobar"

// (1.1)
let pfloatinparens = pchar '(' >>. pfloat .>> pchar ')'
run pfloatinparens "(1.1)"
let pfloatinparens' = between (pchar '(') (pchar ')') pfloat
run pfloatinparens' "(1.1)"



// parse 1.1, 3.7
let plistoffloats = 
    (sepBy pfloat (pchar ',' .>> spaces))
run plistoffloats "1.1, 3.7"

// (1.1, 3.7)
type Point = {x: float; y: float}

let ppoint = between (pchar '(') (pchar ')') plistoffloats |>> (fun (x::y::xs) -> {x = x; y = y})
run ppoint "(1.1, 3.7)"

let plistoffloats' = 
    pipe3 pfloat (pchar ',' .>> spaces) pfloat (fun x z y -> {x = x; y = y})
run plistoffloats' "1.1, 3.7"

let ppoint' = 
    between 
     (pchar '(')
     (pchar ')') 
     plistoffloats'
run ppoint' "(1.1, 3.7)"

let fermats = seq { for x in 1..4 do yield pown 2 (pown 2 x) }
Seq.take 4 fermats |> Seq.toList
