# AOC 2019 :hash: sharp and functional :hash:

## About

Selected solutions from the original [Python repo](https://github.com/tranzystorek-io/aoc2019), now in stylish F#.

F# is an opportunity to learn both .NET and the functional pardigm.

## Dependencies

To run F# from this repo, you will need the [.NET Core SDK](https://dotnet.microsoft.com/download).

## Usage

The solutions are all F# projects in the [src/days/](src/days) directory.

To check that everything works fine, you can try to build the whole repository:

`dotnet build`

As an example, to run the Day01Part1 program, execute the following:

`dotnet run -p src/days/Day01Part1/Day01Part1.fsproj [<INPUT_FILE>]`

## [ArgParse](src/ArgParse) library

Unifying input is surprisingly easy in F#, as both text files and STDIN can be represented
as `IEnumerable<string>` of the lines contained within them.

For argument parsing, I used the [Argu](https://fsprojects.github.io/Argu/) library.
