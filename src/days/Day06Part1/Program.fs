﻿// Learn more about F# at http://fsharp.org

open FSharpx.Collections

open ArgParse

let parseInput args =
    let parse = parseLines "Day 6: Universal Orbit Map - Part 1"
    let lines = parse args

    let splitIntoTuple (line:string) =
        let split = line.Split(')', 2)
        split.[0], split.[1]

    let gravsAndOrbs (grav, pairs) =
        let orbList = pairs |> Seq.map snd |> Seq.toList
        grav, orbList

    lines |> Seq.map splitIntoTuple |> Seq.groupBy fst |> Seq.map gravsAndOrbs |> Map.ofSeq

let getOrbitCount (orbits:Map<string, string list>) =
    let rec loopCount sum searchspace =
        match Seq.unCons searchspace with
        | None -> sum
        | Some((level, grav), tail) ->
            let currentLevel = level + 1
            let currentSum = sum + level
            let orbiters =
                orbits |> Map.findOrDefault grav []
                |> Seq.map (fun orb -> currentLevel, orb)

            loopCount currentSum (Seq.append orbiters tail)

    loopCount 0 [0, "COM"]

[<EntryPoint>]
let main argv =
    let orbits = parseInput argv

    let solution = getOrbitCount orbits

    printfn $"{solution}"
    0 // return an integer exit code
