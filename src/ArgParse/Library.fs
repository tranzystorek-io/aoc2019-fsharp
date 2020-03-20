module ArgParse

open System
open System.IO

open Argu

type AocArguments =
    | [<MainCommand>] InputFile of filepath:string
    interface IArgParserTemplate with
        member s.Usage =
            match s with
            | InputFile _ -> "input file (defaults to STDIN if not provided)"

let stdinLines () =
    Seq.initInfinite (ignore >> Console.ReadLine)
        |> Seq.takeWhile ((<>) null)

let errorHandler =
    let colorizer = function
        | ErrorCode.HelpText -> None
        | _ -> Some ConsoleColor.Red

    ProcessExiter(colorizer = colorizer)

let parseLines description args =
    let parser = ArgumentParser.Create<AocArguments>(helpTextMessage = description, errorHandler = errorHandler)
    let result = parser.ParseCommandLine args

    match result.TryGetResult InputFile with
    | Some(filepath) -> File.ReadLines filepath
    | None -> stdinLines()
