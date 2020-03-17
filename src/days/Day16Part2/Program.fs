// Learn more about F# at http://fsharp.org

open System.Globalization

open ArgParse

let parseInput args =
    let parse = parseLines "Day 16: Flawed Frequency Transmission - Part 2"
    let lines = parse args

    let line = Seq.head lines

    let result = line |> Seq.map CharUnicodeInfo.GetDecimalDigitValue |> Seq.toList

    (line, result)

let computePhase data =
    let reversePartialSums =
        data
        |> Seq.rev
        |> Seq.scan (+) 0

    reversePartialSums
        |> Seq.map (fun v -> v % 10)
        |> Seq.rev
        |> Seq.toList

let runPhases initial n =
    let step state =
        let next = computePhase state
        Some(next, next)

    Seq.unfold step initial |> Seq.item (n - 1)

[<EntryPoint>]
let main argv =
    let (text, receivedData) = parseInput argv

    let messageOffset = text.[..6] |> int

    let realData =
        seq { for _ in 1 .. 10000 do yield! receivedData }
        |> Seq.skip messageOffset
        |> Seq.toList

    let computed = runPhases realData 100
    let solution = computed.[..7] |> Seq.map string |> String.concat ""

    printfn "%s" solution
    0 // return an integer exit code
