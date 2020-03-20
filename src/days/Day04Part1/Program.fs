// Learn more about F# at http://fsharp.org

open FSharpx.Collections

open ArgParse

let parseInput args =
    let parse = parseLines "Day 4: Secure Container - Part 1"
    let lines = parse args

    let line = Seq.head lines

    let split = line.Split('-', 2) |> Seq.map int |> Seq.toArray
    split.[0], split.[1]

let isValid password =
    let rec loopCheck found pairs =
        match Seq.unCons pairs with
        | None -> found
        | Some((a, b), tail) ->
            if a > b then
                false
            else
                loopCheck (found || a = b) tail

    password |> Seq.pairwise |> loopCheck false

[<EntryPoint>]
let main argv =
    let lower, upper = parseInput argv

    let solution = seq { lower .. upper } |> Seq.map string |> Seq.filter isValid |> Seq.length

    printfn "%i" solution
    0 // return an integer exit code
