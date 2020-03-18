// Learn more about F# at http://fsharp.org

open ArgParse

let fuelReq value = value / 3 - 2

let fuelReqChained initialValue =
    let gen state =
        let next = fuelReq state
        Some(next, next)

    Seq.unfold gen initialValue |> Seq.takeWhile (fun v -> v > 0) |> Seq.sum

[<EntryPoint>]
let main argv =
    let parse = parseLines "Day 1: The Tyranny of the Rocket Equation - Part 2"
    let lines = parse argv

    let solution = lines |> Seq.sumBy (int >> fuelReqChained)

    printfn "%i" solution
    0 // return an integer exit code
