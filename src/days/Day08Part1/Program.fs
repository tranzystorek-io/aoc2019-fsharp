// Learn more about F# at http://fsharp.org

open System.Globalization

open ArgParse

let parseInput args =
    let parse = parseLines "Day 8: Space Image Format - Part 1"
    let lines = parse args

    let line = Seq.head lines

    line |> Seq.map CharUnicodeInfo.GetDecimalDigitValue |> Seq.toList

let getLayers =
    let w, h = 25, 6
    Seq.chunkBySize (w * h) >> Seq.map Seq.toList

[<EntryPoint>]
let main argv =
    let data = parseInput argv

    let layers = getLayers data

    let seekedLayer = layers |> Seq.minBy (Seq.filter ((=) 0) >> Seq.length)

    let countElems n = Seq.filter ((=) n) >> Seq.length
    let ones = countElems 1 seekedLayer
    let twos = countElems 2 seekedLayer

    let solution = ones * twos

    printfn "%i" solution
    0 // return an integer exit code
