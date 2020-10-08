//List 
let simpleList = [1; 2; 3]
1 :: [2; 3]

//List comprehensions
[for x in 1..10 do yield 2 * x]

[
    for r in 1..8 do
    for c in 1..8 do 
        if r <> c then
            yield (r, c)
]

//Arrays. Arrays contain pipes in between the square brackets
//Arrays can be indexed and sliced like Strings, however there is no cons operator
let simpleArray = [|1;2;3|]
simpleArray. [1]

[|for x in [1..3]
    do yield 2 * x|]


//Tuples - Group of unnamed, ordered values. These values can be different types.
fst ("Bob", 55)
snd ("Bob", 55)

//By for 3 or more elements, the way you access them is by pattern matching.
let (name, age) = ("Bob", 55)

//Records. Simple aggregate of named values
type Person = {
    name: string;
    age: int
}

let bob = {name = "Bob"; age = 55}

//Records can hold methods as well as properties
type Person1 = {
    name1: string;
    age1: int
} with member this.canDrive = this.age1 > 17

{name1 = "Bob"; age1 = 55}.canDrive

//You can also modify records
{bob with age = 56}

//Discriminated Unions. 
//Types that can take different forms, also called sum types. Can have methods and properties

type Day = Sunday | Monday | Tuesday | Wednesday | Thursday | Friday | Saturday 

//combining Discriminated unions
type Note = A | ASharp | B | C | CSharp | D | DSharp | E | F | FSharp | G | GSharp
type Octave = One | Two | Three

type Sound = Rest | Tone of Note * Octave
Rest
Tone (C, Two)

match Tone (C, Two) with 
    | Tone (note, octave) -> sprintf "%A %A" note octave
    | Rest -> "---"

type Sound1 = Rest | Tone of note: Note * octave: Octave