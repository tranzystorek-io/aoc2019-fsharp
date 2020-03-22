// Learn more about F# at http://fsharp.org

open System.Globalization

open ArgParse

let parseInput args =
    let parse = parseLines "Day 8: Space Image Format - Part 2"
    let lines = parse args

    let line = Seq.head lines

    line |> Seq.map CharUnicodeInfo.GetDecimalDigitValue |> Seq.toList

let decode img =
    let stepZip state =
        if state |> Seq.forall Seq.isEmpty then
            None
        else
            let heads = state |> Seq.map Seq.head |> Seq.toList
            let tails = state |> Seq.map Seq.tail
            Some(heads, tails)

    let w, h = 25, 6
    let layerSize = w * h
    let layers = img |> Seq.chunkBySize layerSize |> Seq.map Seq.ofArray

    layers |> Seq.unfold stepZip |> Seq.map (Seq.skipWhile ((=) 2) >> Seq.head)

[<EntryPoint>]
let main argv =
    let data = parseInput argv

    let decoded = decode data

    let mapColor = function
        | 0 -> ' '
        | 1 -> '#'
        | _ -> failwith "Unrecognized pixel color"

    let width = 25
    decoded
        |> Seq.map mapColor
        |> Seq.chunkBySize width
        |> Seq.map System.String
        |> Seq.iter (printfn "%s")
    0 // return an integer exit code
