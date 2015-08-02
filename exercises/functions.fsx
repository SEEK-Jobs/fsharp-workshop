(*
F# is a functional language - functions are important
In this first set of exercises we are going to write some
simple functions and see how to combine them
 *)
#load "./examples.fs"

// this is NOT a function this is just an
// integer
let a = 1

// functions are first class values in F# so
// the syntax is idential to the above except that
// it has a parameter
let incriment a = a + 1

// notice that there no brackets when calling
// the incriment function. calling a function
// is so important that it requires no extra syntax
Examples.test "incriment 10 equals 11" (fun () ->
  incriment 10 = 11
)

// types are mostly optional in F#, the compiler
// does a very good job in infering types when
// we leave them off. Here is the addOne function
// above with the type shown explicitely
// NOTE: there is nothing special about the ' it's just
// a common convention for defining something that is related
// or similar to a previous definition
let incriment' (a : int) : int = a + 1

// F# also allows functions to be defined inline
// using the fun keyword. This is similar to the lambda
// syntax in C# (a => a + 1)
let incriment'' = fun a -> a + 1

// F# uses -> to indicate function types
// the type of int -> int says that the function
// takes a single integer and returns an integer
let incriment''' : int -> int = fun a -> a + 1

// add is a function of two parameters
let add a b = a + b

Examples.test "Can add two numbers" (fun () ->
  add 1 2 = 3
)

// the type syntax might not be what you expected
let add' : int -> int -> int = add

// functions only really take one argument at a time
// so you can think of (int -> int -> int) as really
// a type of (int -> (int -> int))
// ie a function of two parameters is really a function
// that takes one parameter and returns a function that
// takes on parameter
let incriment'''' : int -> int = add 1

// passing functions as arguments to other functions
// is a really powerful technique. This is a silly example.
let applyFunctionThenAdd2 f n =
    (f n) + 2

Examples.test "Multiply by two then add two" (fun () ->
    applyFunctionThenAdd2 (fun x -> x * 2) 10 = 22
)

// the pipe operator (|>) allows us to chain operations together
// for example if we wanted to implement add (incriment (n + 10)) 20
// we could do it like:
let examplePipe n =
    n
    |> add 10
    |> incriment
    |> add 20

// the previous value is passed as the last argument to the next
// function.
// implement this example using the pipe operator
// add 40 (add 20 (incriment n))
let examplePipe2 n =
    n
    |> incriment
    |> add 20
    |> add 40

Examples.test "add 40 (add 20 (incriment n))" (fun () ->
    examplePipe2 1 = 62
)

// once nice thing about functions is that they can be nested
let outerFunction n =
  let multiply x = n * x // this is a function only available inside outerFunction
  multiply 10 + 3

// the pipe operator has a very simple implementation
// try implementing ||> to do the same things as pipe
let (||>) x f = f x

Examples.test "Custom pipe" (fun () -> 
    10
    ||> (add 2)
    ||> incriment
    ||> (fun x -> x = 13)
)
