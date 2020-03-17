// Learn more about F# at http://fsharp.org

open System

open ArgParse

let fuelReq value = value / 3 - 2

[<EntryPoint>]
let main argv =
    let parse = parseLines "Day 1: The Tyranny of the Rocket Equation - Part 1"
    let lines = parse argv

    let solution = lines |> Seq.sumBy (int >> fuelReq)

    printfn "%i" solution
    0 // return an integer exit code
