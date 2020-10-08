#load "C:/Users/gvida/.nuget/packages/fsharp.charting/2.1.0/FSharp.Charting.fsx"

open FSharp.Charting

let linear x = 2.0 * x 
[for x in 1.00 .. 100.00 -> (x, linear x)] |> Chart.Line
