namespace ScreenLockPatternCalc

#l "Utils.fs"
open FSharp.Collections.ParallelSeq
open Utils

module LockPatternPermCounter = 

    let upperLimitEstimate (a:int) (b:int) =
        let count = (a*b)
        seq {for i in 1..count -> (p count i)} |> Seq.fold (+) (bigint 0)

    let allDistinctPerms rows columns maxThreads =
        let toCoords ind =
            Coords(ind / columns, ind % columns)
        let toIndex (c:Coords) =
            c.x*columns + c.y
        let nodeCount = rows*columns

        let indices = [for i in 1..nodeCount -> i-1]

        let tailHasNoExtraNodes a b perm =
            let aC = toCoords a
            let bC = toCoords b
            let diff = bC - aC
            let diffGcd = diff.asTuple ||> gcd |> abs
            if diffGcd = 1 then
                true
            else
                let d = diff / diffGcd
                let fullHead = 
                    seq {for i in 0..diffGcd -> aC + (d*i)}
                    |> Seq.map toIndex
                    |> Seq.toList
                ((Set.ofList fullHead) - (Set.ofList perm) = Set.empty)

        let keepPerm perm =
            // All but newest two nodes have been checked for extra nodes
            match perm with
            | a::[] ->  true
            | [] ->  true
            | a::b::tail ->  tailHasNoExtraNodes a b perm

        let followingPerms perm = 
            indices
            |> List.except perm
            |> List.map (fun i -> i::perm) 
            // Remove perms where the newest jumps over a node not already crossed
            |> List.filter keepPerm

        let rec perms ps =
            seq { for p in ps do
                    yield p
                    yield! followingPerms p |> perms
            }

        followingPerms []
        // Parallelize, split into maxThreads evenly sized (as possible) groups
        |> List.indexed
        |> List.groupBy (fst >> (fun i -> i % maxThreads))
        |> List.map (fun (m,list) -> List.map snd list)
        // Generate perms in parallel
        |> PSeq.collect perms 

    let calculateLockPatternPermCount rows columns maxThreads =
        allDistinctPerms rows columns maxThreads |> PSeq.length

