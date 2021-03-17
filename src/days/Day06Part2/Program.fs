// Learn more about F# at http://fsharp.org

open FSharpx.Collections

open ArgParse

let parseInput args =
    let parse = parseLines "Day 6: Universal Orbit Map - Part 2"
    let lines = parse args

    let splitIntoTuple (line:string) =
        let split = line.Split(')', 2)
        split.[0], split.[1]

    let gravsAndOrbs (grav, pairs) =
        let orbList = pairs |> Seq.map snd |> Seq.toList
        grav, orbList

    lines |> Seq.map splitIntoTuple |> Seq.groupBy fst |> Seq.map gravsAndOrbs |> Map.ofSeq

let findPath (orbits:Map<string, string list>) destination =
    let rec loopFind searchspace =
        match Seq.unCons searchspace with
        | None -> None
        | Some((path, grav), _) when grav = destination -> Some path
        | Some((path, grav), tail) ->
            let currentPath = path @ [grav]
            let orbiters =
                orbits |> Map.findOrDefault grav []
                |> Seq.map (fun orb -> currentPath, orb)

            loopFind (Seq.append orbiters tail)

    loopFind [[], "COM"]

let zipLongest defaultValue (a:'a list) (b:'a list) =
    let fill =
        let aLen = a.Length
        let bLen = b.Length
        if aLen > bLen then
            a |> List.skip bLen |> Seq.map (fun el -> el, defaultValue)
        else
            b |> List.skip aLen |> Seq.map (fun el -> defaultValue, el)

    let shortZip = Seq.zip a b
    Seq.append shortZip fill

[<EntryPoint>]
let main argv =
    let orbits = parseInput argv
    let findPathTo = findPath orbits

    let mePath = findPathTo "YOU" |> Option.get
    let santaPath = findPathTo "SAN" |> Option.get

    let zipped = zipLongest null mePath santaPath
    let meDiv, santaDiv = zipped |> Seq.skipWhile ((<||) (=)) |> List.ofSeq |> List.unzip

    let getEffectiveLength = Seq.takeWhile ((<>) null) >> Seq.length
    let meLength = getEffectiveLength meDiv
    let santaLength = getEffectiveLength santaDiv

    let solution = meLength + santaLength

    printfn $"{solution}"
    0 // return an integer exit code
