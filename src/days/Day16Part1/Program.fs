// Learn more about F# at http://fsharp.org

open System
open System.Globalization
open System.Linq

open ArgParse

let parseInput args =
    let parse = parseLines "Day 16: Flawed Frequency Transmission - Part 1"
    let lines = parse args

    let line = Seq.head lines

    line |> Seq.map CharUnicodeInfo.GetDecimalDigitValue |> Seq.toList

let getPattern position =
    let rec cycle s = seq { yield! s; yield! cycle s }

    [0; 1; 0; -1]
        |> Seq.collect (fun el -> Enumerable.Repeat (el, position))
        |> cycle
        |> Seq.skip 1

let computePhase (data: int list) =
    let nums = Seq.unfold (fun state -> Some(state, state + 1)) 1
    let processPosition pos =
        let pattern = getPattern pos
        let summed = data |> Seq.zip pattern |> Seq.sumBy (fun (v, p) -> v * p)

        (abs summed) % 10

    nums |> Seq.take data.Length |> Seq.map processPosition |> Seq.toList

let runPhases initial n =
    let step state =
        let next = computePhase state
        Some(next, next)

    Seq.unfold step initial |> Seq.item (n - 1)

[<EntryPoint>]
let main argv =
    let data = parseInput argv

    let computed = runPhases data 100
    let solution = computed.[..7] |> Seq.map string |> String.concat ""

    printfn "%s" solution
    0 // return an integer exit code
