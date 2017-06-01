namespace ScreenLockPatternCalc

#l "LockPatternPermCounter.fs"
#l "Utils.fs"
open LockPatternPermCounter
open Utils

module Program = 

    [<EntryPoint>]
    let main argv = 
        match argv with
        | [|Integer a; Integer b|] when a > 0 && b > 0 ->
            if Settings.PrintUpperEstimate then
                upperLimitEstimate a b |> printfn "Upper limit of %A distinct permutations"

            let stopwatch = System.Diagnostics.Stopwatch.StartNew()

            calculateLockPatternPermCount a b
            |> printfn "A %A by %A grid yields %A distinct possible permutations" a b
            
            if Settings.PrintTime then
                printfn "Calculations took %A" (stopwatch.Elapsed)

            0
        | _ -> failwith "arguments must be two positive integers"
