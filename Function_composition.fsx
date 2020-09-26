// given a function x : ('a -> 'b)
// and a function y : ('b -> 'c)

//x >> y
//y << x
//both produce a function : ('a -> 'c)

let minus1 x = x - 1
let times2 = (*) 2

minus1 9
times2 8

// Different ways you can approach this
times2(minus1 9)
let minus1ThenTimes2 = times2 << minus1
minus1ThenTimes2 7
(times2 << minus1) 9
times2 << minus1 <| 9