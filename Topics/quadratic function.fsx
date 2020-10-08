#load "C:/Users/gvida/.nuget/packages/fsharp.charting/2.1.0/FSharp.Charting.fsx"

open FSharp.Charting

let quadratic x = x ** 2.0
[for x in -10.0 .. 0.1 .. 10.0 -> (x, quadratic x)] |> Chart.Line
