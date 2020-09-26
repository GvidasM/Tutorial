// A factorial function
let rec factorial n = 
    if n < 2 then
        1
    else
    n * factorial (n-1) 

// length calculator
let rec length = function
    | [] -> 0
    | x::xs -> 1 + length xs