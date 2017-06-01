namespace ScreenLockPatternCalc

#l "LockPatternPermCounter.fs"
open LockPatternPermCounter

module Program = 

    [<EntryPoint>]
    let main argv = 
        match argv with
        | [|Integer a; Integer b|] when a > 0 && b > 0 ->
            memcost a b |> printfn "Upper limit of %A distinct permutations"
            let start = System.DateTime.Now;
            CalculateLockPatternPermCount a b |> printfn "A %A by %A grid yields %A distinct possible permutations" a b
            printfn "Calculations took %A" (System.DateTime.Now - start)
            0
        | _ -> failwith "arguments must be two positive integers"
