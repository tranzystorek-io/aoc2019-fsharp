// Learn more about F# at http://fsharp.org

open FSharpx.Collections

open ArgParse

let parseInput args =
    let parse = parseLines "Day 4: Secure Container - Part 2"
    let lines = parse args

    let line = Seq.head lines

    let split = line.Split('-', 2) |> Seq.map int |> Seq.toArray
    split.[0], split.[1]

let isValid password =
    let rec loopCheck adjacentLength found pairs =
        match Seq.unCons pairs with
        | None -> found || adjacentLength = 2
        | Some((a, b), _) when a > b -> false
        | Some((a, b), tail) when a = b ->
            let currentLength = adjacentLength + 1
            loopCheck currentLength found tail
        | Some(_, tail) ->
            let currentFound = found || adjacentLength = 2
            loopCheck 1 currentFound tail

    password |> Seq.pairwise |> loopCheck 1 false

[<EntryPoint>]
let main argv =
    let lower, upper = parseInput argv

    let solution = seq { lower .. upper } |> Seq.map string |> Seq.filter isValid |> Seq.length

    printfn $"{solution}"
    0 // return an integer exit code
