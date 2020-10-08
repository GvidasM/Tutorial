//Get specific character(-s) from a String
let intro = "It was the best of times,"
intro. [0]
intro. [1]

intro. [3..5]

//String module
String.forall System.Char.IsDigit "03249"

String.init 10 (fun i -> i * 10 |> string)